using RC.gRPC.Client.API.Models;
using RC.gRPC.Client.API.Protos;

namespace RC.gRPC.Client.API.Services
{
    public class CatalogGrpcService : ICatalogGrpcService
    {
        private readonly Catalog.CatalogClient _client;

        public CatalogGrpcService(Catalog.CatalogClient client)
        {
            _client = client;
        }

        public async Task<List<ProductDto>> ListProductsAsync()
        {
            var response = await _client.ListProductsAsync(new ListProductsRequest());

            return MapProtoResponseToListProductsDto(response);
        }

        private static List<ProductDto> MapProtoResponseToListProductsDto(ListProductsResponse response)
        {
            var listProducts = new List<ProductDto>();

            foreach (var product in response.Products)
            {
                listProducts.Add(new ProductDto()
                {
                    Id = product.Id,
                    ProductGuid = product.Productguid,
                    Name = product.Name,
                    Description = product.Description,
                    Value = (decimal)product.Value,
                    Quantity = product.Quantity,
                    IsActive = product.Isactive
                });
            }

            return listProducts;
        }
    }
}