using Grpc.Core;
using RC.gRPC.Server.API.Models;
using RC.gRPC.Server.API.Protos;

namespace RC.gRPC.Server.API.Services
{
    // A implementação do serviço gRPC
    // que obrigatoriamente PRECISA herdar da classe do protobuf
    // O protobuf é como se fosse uma interface/schema
    public class CatalogGrpcService : Catalog.CatalogBase // Classe gerada pelo compilador
    {
        // Implementação do método
        public override Task<ListProductsResponse> ListProducts(ListProductsRequest request, ServerCallContext context)
        {
            // Simulando serviços reais como Repositories, Loggers, etc...
            // Neste cenário, estou fingindo que busco os produtos da base, usando um repository

            var products = new List<Product>();

            for (int i = 1; i <= 3; i++)
            {
                products.Add(new Product($"Product {i}", $"Product Description {i}", i * 32.5m, i));
            }

            return Task.FromResult(MapListProductsToProtoResponse(products));
        }

        private static ListProductsResponse MapListProductsToProtoResponse(List<Product> products)
        {
            var protoResponse = new ListProductsResponse();

            foreach (var product in products)
            {
                protoResponse.Products.Add(new ProductResponse()
                {
                    Id = Guid.NewGuid().ToString(),
                    Productguid = Guid.NewGuid().ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Value = (double)product.Value,
                    Quantity = product.Quantity,
                    Isactive = product.IsActive
                });
            }

            return protoResponse;
        }
    }
}
