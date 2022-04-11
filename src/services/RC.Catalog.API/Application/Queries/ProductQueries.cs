using RC.Catalog.API.Application.DTO;
using RC.Catalog.API.Data.Repositories;

namespace RC.Catalog.API.Application.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;

        public ProductQueries(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var productList = _productRepository.GetAll();

            return productList.Select(ProductDTO.ToProductDTO);
        }
    }
}
