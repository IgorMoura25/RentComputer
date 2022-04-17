using FluentValidation;
using RC.Core.Messages;

namespace RC.Catalog.API.Application.Commands
{
    public class AddProductCommand : MediatRCommand
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }

        public AddProductCommand(long id, string name, string description, decimal value, int quantity)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddProductCommandValidation : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidation()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("The name of the product was not supplied");

            RuleFor(product => product.Value)
               .GreaterThan(0)
               .WithMessage("The value of the product must be greater than zero");

            RuleFor(product => product.Quantity)
               .Must(HasValidQuantity)
               .WithMessage("The quantity of the product must be greater than zero");
        }

        public bool HasValidQuantity(int quantity)
        {
            return quantity > 0;
        }
    }
}
