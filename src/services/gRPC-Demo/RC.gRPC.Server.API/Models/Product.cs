namespace RC.gRPC.Server.API.Models
{
    public class Product
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public int Quantity { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Product(string name, string description, decimal value, int quantity)
        {
            Name = name;
            Description = description;
            Value = value;
            Quantity = quantity;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
