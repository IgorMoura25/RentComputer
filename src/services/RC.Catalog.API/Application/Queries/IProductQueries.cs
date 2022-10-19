using RC.Catalog.API.Data.DTO;

namespace RC.Catalog.API.Application.Queries
{
    public interface IProductQueries
    {
        public Task<IEnumerable<ProductDTO>> GetAllAsync();
        public Task<ProductDTO?> GetByGuidAsync(Guid guid);
    }
}
