using RC.Catalog.API.Domain;

namespace RC.Catalog.API.Application.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<ProductImageDTO> Images { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ProductDTO ToProductDTO(Product product)
        {
            if (product == null) return null;

            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Quantity = product.Quantity,
                Images = product.Images?.ToList().ConvertAll(x => new ProductImageDTO()
                {
                    Path = x.Path
                }),
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };
        }
    }

    public class ProductImageDTO
    {
        public string Path { get; set; }
    }
}
