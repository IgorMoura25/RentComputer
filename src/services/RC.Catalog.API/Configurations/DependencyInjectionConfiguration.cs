using FluentValidation.Results;
using MediatR;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.Catalog.API.Data;
using RC.Catalog.API.Data.Dapper;
using RC.Catalog.API.Data.Repositories;
using RC.Core.Data;
using RC.Core.Mediator;

namespace RC.Catalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession>(service => new SqlServerDbSession(configuration.GetConnectionString("SqlServer")));
            services.AddTransient<IUnitOfWork, SqlServerUnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDapperProcedureExecution, DapperProcedureExecution>();

            services.AddScoped<IProductQueries, ProductQueries>();

            services.AddScoped<IMediatRHandler, MediatRHandler>();
            services.AddScoped<IRequestHandler<AddProductCommand, ValidationResult>, ProductCommandHandler>();
        }
    }
}
