using RC.Catalog.API.Application.Queries;
using RC.Catalog.API.Data.Repositories;

namespace RC.Catalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            services.AddScoped<IProductQueries, ProductQueries>();
        }
    }
}
