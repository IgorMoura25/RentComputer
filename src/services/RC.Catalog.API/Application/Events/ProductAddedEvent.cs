using RC.Core.Messages;

namespace RC.Catalog.API.Application.Events
{
    public class ProductAddedEvent : MediatREvent
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }

        public ProductAddedEvent(long id, string name, string description, decimal value, int quantity)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
        }
    }
}
