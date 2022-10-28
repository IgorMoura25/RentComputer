using RC.Catalog.API.Data.DTO;
using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductQueryRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByNameAsync(string name);
        Task<ProductDTO?> GetByGuidAsync(Guid guid);
        Task<ProductDTO?> UpdateByGuidAsync(Guid guid, ProductDTO newProduct);
        Task CreateAsync(ProductDTO product);
    }
}
