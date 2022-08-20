using RC.gRPC.Client.API.Protos;
using RC.gRPC.Client.API.Services;

namespace RC.gRPC.Client.API.Configurations
{
    public static class GrpcConfiguration
    {
        public static IServiceCollection ConfigureGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICatalogGrpcService, CatalogGrpcService>();

            services.AddGrpcClient<Catalog.CatalogClient>(options =>
            {
                options.Address = new Uri("https://localhost:7298");
            });

            return services;
        }
    }
}
