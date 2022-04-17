using RC.Core.Data;
using RC.Core.Data.Dapper;
using RC.Customer.API.Application.Queries;
using RC.Customer.API.Data;
using RC.Customer.API.Data.Repositories;

namespace RC.Customer.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession>(service => new SqlServerDbSession(configuration.GetConnectionString("SqlServer")));
            services.AddTransient<IUnitOfWork, SqlServerUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDapperProcedureExecution, DapperProcedureExecution>();

            services.AddScoped<ICustomerQueries, CustomerQueries>();
        }
    }
}
