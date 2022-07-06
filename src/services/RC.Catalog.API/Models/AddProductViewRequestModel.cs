namespace RC.Catalog.API.Models
{
    public class AddProductViewRequestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
