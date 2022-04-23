using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductCommandRepository : IRepository<Product>
    {
        Product Add(Product model);
    }
}
