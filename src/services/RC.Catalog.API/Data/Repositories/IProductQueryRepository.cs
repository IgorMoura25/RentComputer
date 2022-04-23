using RC.Catalog.API.Data.DTO;
using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductQueryRepository : IRepository<Product>
    {
        public Task<IEnumerable<ProductDTO>> GetAllAsync();
        public Task<ProductDTO?> GetByNameAsync(string name);
        public Task CreateAsync(ProductDTO product);
    }
}
