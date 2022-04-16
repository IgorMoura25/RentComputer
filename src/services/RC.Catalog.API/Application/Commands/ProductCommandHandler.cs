using FluentValidation.Results;
using MediatR;
using RC.Catalog.API.Application.Events;
using RC.Catalog.API.Data.Repositories;
using RC.Catalog.API.Domain;
using RC.Core.Data;
using RC.Core.Mediator;
using RC.Core.Messages;

namespace RC.Catalog.API.Application.Commands
{
    public class ProductCommandHandler : CommandHandler, IRequestHandler<AddProductCommand, ValidationResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MediatREventList _eventList;

        public ProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, MediatREventList eventList)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _eventList = eventList;
        }

        public async Task<ValidationResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var product = new Product(request.Name, request.Description, request.Value, request.Quantity);

            var storedProduct = _productRepository.GetByName(product.Name);

            if (storedProduct != null)
            {
                AddError("Product already exists");
                return ValidationResult;
            }

            await _unitOfWork.BeginTransaction();

            try
            {
                product = _productRepository.Add(product);

                _eventList.AddEvent(new ProductAddedEvent(product.Id, product.Name, product.Description, product.Value, product.Quantity));

                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
                AddError("An error ocurred while creating the product");
                return ValidationResult;
            }

            return request.ValidationResult;
        }
    }
}
