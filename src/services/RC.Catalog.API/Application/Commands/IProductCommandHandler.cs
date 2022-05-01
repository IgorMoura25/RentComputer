using FluentValidation.Results;

namespace RC.Catalog.API.Application.Commands
{
    public interface IProductCommandHandler
    {
        public Task<ValidationResult> AddProduct(AddProductCommand request);
    }
}
