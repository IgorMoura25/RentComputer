using FluentValidation.Results;
using MediatR;

namespace RC.Catalog.API.Application.Commands
{
    public class ProductCommandHandler : IRequestHandler<AddProductCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            // TODO: Continuar daqui...

            throw new NotImplementedException();
        }
    }
}
