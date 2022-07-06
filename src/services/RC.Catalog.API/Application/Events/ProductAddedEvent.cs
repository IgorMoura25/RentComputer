using RC.Core.Messages;

namespace RC.Catalog.API.Application.Events
{
    public class ProductAddedEvent : MediatREvent
    {
        public long Id { get; set; }
        public Guid ProductGuid { get; set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductAddedEvent(long id, Guid productGuid, string? name, string? description, decimal value, int quantity, bool isActive, DateTime createdAt)
        {
            AggregateId = id;
            Id = id;
            ProductGuid = productGuid;
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            IsActive = isActive;
            CreatedAt = createdAt;
        }
    }
}
