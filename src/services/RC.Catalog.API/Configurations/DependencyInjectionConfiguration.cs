using RC.Catalog.API.Application.Queries;
using RC.Catalog.API.Data;
using RC.Catalog.API.Data.Repositories;
using RC.Core.Data;
using RC.Core.Data.Dapper;

namespace RC.Catalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession>(service => new SqlServerDbSession(configuration.GetSection("DataBaseSettings:WriteConnectionString").Value));
            services.AddTransient<IUnitOfWork, SqlServerUnitOfWork>();
            services.AddScoped<IDapperProcedureExecution, DapperProcedureExecution>();
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            services.AddScoped<IProductQueries, ProductQueries>();
        }
    }
}
