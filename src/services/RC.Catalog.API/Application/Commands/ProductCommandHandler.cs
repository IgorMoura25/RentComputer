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

            _eventList.AddEvent(new ProductAddedEvent(product.Id, product.Name, product.Description, product.Value, product.Quantity));

            var success = await _productRepository.UnitOfWork.CommitAsync();

            if (!success)
            {
                AddError("An error ocurred while creating the product");
                return ValidationResult;
            }

            return ValidationResult;
        }
    }
}
