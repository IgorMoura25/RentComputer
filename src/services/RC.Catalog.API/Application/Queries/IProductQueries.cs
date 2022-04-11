using RC.Catalog.API.Application.DTO;

namespace RC.Catalog.API.Application.Queries
{
    public interface IProductQueries
    {
        IEnumerable<ProductDTO> GetAll();
    }
}
