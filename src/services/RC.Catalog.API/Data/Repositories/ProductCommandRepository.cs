using Microsoft.EntityFrameworkCore;
using RC.Catalog.API.Domain;
using RC.Core.Data;

namespace RC.Catalog.API.Data.Repositories
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductCommandRepository(CatalogContext context)
        {
            _context = context;
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);

            return product;
        }

        public ProductImage AddImage(ProductImage image)
        {
            _context.ProductImages.Add(image);

            return image;
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product?> GetByGuidAsync(Guid guid)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.UniversalId == guid);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
