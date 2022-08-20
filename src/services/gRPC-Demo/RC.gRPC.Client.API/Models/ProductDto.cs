namespace RC.gRPC.Client.API.Models
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public string? ProductGuid { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
