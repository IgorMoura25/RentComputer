using Microsoft.EntityFrameworkCore;
using RC.Catalog.API.Data.Repositories;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.Catalog.API.Services;
using RC.Catalog.API.Data;
using RC.MessageBus;
using RC.MessageBus.EasyNetQ;

namespace RC.Catalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<EventList>();
            services.AddHostedService<CommandBusHandler>();

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
            services.AddScoped<IProductCommandHandler, ProductCommandHandler>();

            return services;
        }
    }
}
