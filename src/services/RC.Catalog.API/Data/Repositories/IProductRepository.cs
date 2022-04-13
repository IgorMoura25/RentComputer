using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAll();
        Product GetByName(string name);
        Product Add(Product model);
    }
}
