using RC.Core.DomainObjects;

namespace RC.Catalog.API.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }
        public IEnumerable<ProductImage> Images { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Product()
        {
        }

        public Product(string name, string description, decimal value, int quantity)
        {
            UniversalId = Guid.NewGuid();
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
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

            if (Value < 0 || Value == 0)
            {
                throw new DomainException("Invalid value");
            }

            if (Quantity < 0 || Quantity == 0)
            {
                throw new DomainException("Invalid quantity");
            }
        }
    }
}
