using RC.Catalog.API.Data.DTO;

namespace RC.Catalog.API.Application.Queries
{
    public interface IProductQueries
    {
        public Task<IEnumerable<ProductDTO>> GetAllAsync();
    }
}
