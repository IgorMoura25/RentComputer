using Microsoft.EntityFrameworkCore;
using RC.Catalog.API.Data.Repositories;
using RC.Catalog.API.Application.Queries;
using RC.Catalog.API.Data;

namespace RC.Catalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services, DataBaseSettings dataBaseSettings)
        {
            services.AddDbContext<CatalogContext>(options => options.UseSqlServer(dataBaseSettings.WriteConnectionString));

            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductQueries, ProductQueries>();

            return services;
        }
    }
}
