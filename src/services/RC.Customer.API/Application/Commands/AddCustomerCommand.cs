using FluentValidation;
using RC.Core.DomainObjects;
using RC.Core.Messages;

namespace RC.Customer.API.Application.Commands
{
    public class AddCustomerCommand : MediatRCommand
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string NationalId { get; private set; }

        public AddCustomerCommand(long id, string name, string email, string nationalId)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            NationalId = nationalId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddCustomerCommandValidation : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidation()
        {
            RuleFor(customer => customer.Name)
                .NotEmpty()
                .WithMessage("The name of the customer was not supplied");

            RuleFor(customer => customer.Email)
               .NotEmpty()
               .WithMessage("The email of the customer was not supplied");

            RuleFor(customer => customer.Email)
               .Must(HaveValidEmail)
               .WithMessage("The customer email is invalid");

            RuleFor(customer => customer.NationalId)
               .Must(HaveValidNationalId)
               .WithMessage("The customer national id is invalid");
        }

        protected static bool HaveValidNationalId(string cpf)
        {
            return NationalId.IsValid(cpf);
        }

        protected static bool HaveValidEmail(string email)
        {
            return Email.IsValid(email);
        }
    }
}
