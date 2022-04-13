namespace RC.Catalog.API.Data.DTO
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
