using Microsoft.EntityFrameworkCore;
using RC.Identity.API.Data;
using RC.Identity.API.Data.Repositories;

namespace RC.Identity.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services, DataBaseSettings dataBaseSettings)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dataBaseSettings.ConnectionString));

            services.AddScoped<ISecurityKeyRepository, SecurityKeyRepository>();

            return services;
        }
    }
}
