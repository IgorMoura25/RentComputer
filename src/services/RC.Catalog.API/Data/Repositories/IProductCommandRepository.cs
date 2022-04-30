using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductCommandRepository : IRepositoryWithUnitOfWork<Product>
    {
        Product Add(Product model);
        public Task<Product?> GetByName(string name);
    }
}
