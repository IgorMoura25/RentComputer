using FluentValidation.Results;
using MediatR;
using RC.Catalog.API.Application.Events;
using RC.Catalog.API.Data.Repositories;
using RC.Catalog.API.Domain;
using RC.Core.Messages;
using RC.MessageBus.Mediator;

namespace RC.Catalog.API.Application.Commands
{
    public class ProductCommandHandler : CommandHandler, IRequestHandler<AddProductCommand, ValidationResult>
    {
        private readonly IProductCommandRepository _productRepository;
        private readonly MediatREventList _eventList;

        public ProductCommandHandler(IProductCommandRepository productRepository, MediatREventList eventList)
        {
            _productRepository = productRepository;
            _eventList = eventList;
        }

        public async Task<ValidationResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var product = new Product(request.Name, request.Description, request.Value, request.Quantity);

            var storedProduct = await _productRepository.GetByNameAsync(product.Name);

            if (storedProduct != null)
            {
                AddError("Product already exists");
                return ValidationResult;
            }

            _productRepository.Add(product);

            _eventList.AddEvent(new ProductAddedEvent(product.Id, product.UniversalId, product.Name, product.Description, product.Value, product.Quantity, product.IsActive, product.CreatedAt));

            var success = await _productRepository.UnitOfWork.CommitAsync();

            if (!success)
            {
                AddError("An error ocurred while creating the product");
                return ValidationResult;
            }

            var addedProduct = await _productRepository.GetByGuidAsync(product.UniversalId);

            var path = UploadFile(request.ImageBase64, request.ImageName);

            if (path == null)
            {
                return ValidationResult;
            }

            var image = new ProductImage(addedProduct.Id, addedProduct.UniversalId, path);

            _productRepository.AddImage(image);

            var splitArray = path.Split("\\");

            var imageName = splitArray.Last();

            _eventList.AddEvent(new ProductImageAddedEvent(image.Id, image.UniversalId, addedProduct.UniversalId, imageName, path));

            var successImage = await _productRepository.UnitOfWork.CommitAsync();

            if (!successImage)
            {
                AddError("An error ocurred while adding the product image");

                // TODO: Should delete the product if the image did not work

                return ValidationResult;
            }

            return ValidationResult;
        }

        private string? UploadFile(string base64, string imageName)
        {
            var bytes = Convert.FromBase64String(base64);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/products", ($"{Guid.NewGuid()}-{imageName}"));

            if (File.Exists(path))
            {
                AddError("File already exists");
                return null;
            }

            File.WriteAllBytes(path, bytes);

            return path;
        }
    }
}
