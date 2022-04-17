using RC.Core.DomainObjects;

namespace RC.Customer.API.Domain
{
    public class BusinessCustomer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public NationalId NationalId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected BusinessCustomer()
        {
        }

        public BusinessCustomer(string name, string email, string nationalId)
        {
            UniversalId = Guid.NewGuid();
            Name = name;
            Email = new Email(email);
            NationalId = new NationalId(nationalId);
            IsActive = true;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Invalid name");
            }
        }

        public void SetEmail(Email email)
        {
            Email = new Email(email?.EmailAddress);
        }

        public void SetNationalId(NationalId nationalId)
        {
            NationalId = new NationalId(nationalId?.Number);
        }
    }
}
