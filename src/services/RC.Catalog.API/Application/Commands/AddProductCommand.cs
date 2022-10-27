using FluentValidation;
using RC.Core.Messages;

namespace RC.Catalog.API.Application.Commands
{
    public class AddProductCommand : MediatRCommand
    {
        public long Id { get; set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }
        public string? ImageName { get; private set; }
        public string? ImageBase64 { get; private set; }

        public AddProductCommand(long id, string? name, string? description, decimal value, int quantity, string? imageName, string? imageBase64)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            ImageName = imageName;
            ImageBase64 = imageBase64;
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

            RuleFor(product => product.ImageName)
               .Must(x => !string.IsNullOrWhiteSpace(x))
               .WithMessage("The name of the product image was not supplied");

            RuleFor(product => product.ImageBase64)
               .Must(IsBase64String)
               .WithMessage("The image of the product is not a base64 format");
        }

        public bool HasValidQuantity(int quantity)
        {
            return quantity > 0;
        }

        public bool IsBase64String(string? base64)
        {
            if (base64 is null) return false;

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
