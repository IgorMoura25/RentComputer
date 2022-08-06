using Microsoft.AspNetCore.Identity;
using RC.Identity.API.Data;

namespace RC.Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
