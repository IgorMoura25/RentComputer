using RC.gRPC.Client.API.Models;

namespace RC.gRPC.Client.API.Services
{
    public interface ICatalogGrpcService
    {
        Task<List<ProductDto>> ListProductsAsync();
    }
}
