using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public interface IProductCommandRepository : IRepositoryWithUnitOfWork<Product>
    {
        Product Add(Product model);
        ProductImage AddImage(ProductImage model);
        Task<Product?> GetByNameAsync(string name);
        Task<Product?> GetByGuidAsync(Guid guid);
    }
}
