using Microsoft.EntityFrameworkCore;
using RC.Identity.API.Data;

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

            return services;
        }
    }
}
