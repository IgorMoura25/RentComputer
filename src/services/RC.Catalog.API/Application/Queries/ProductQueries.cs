using RC.Catalog.API.Data.DTO;
using RC.Catalog.API.Data.Repositories;

namespace RC.Catalog.API.Application.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductQueryRepository _productRepository;

        public ProductQueries(IProductQueryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<ProductDTO?> GetByGuidAsync(Guid guid)
        {
            return await _productRepository.GetByGuidAsync(guid);
        }
    }
}
