using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RC.WebAPI.Core.Auth.Extensions;

namespace RC.WebAPI.Core.Auth
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
                AddJwtBearer(options =>
                {
                    options.SetJwksRetrieverOptions("https://localhost:7241/jwks");
                });

            return services;
        }
    }
}
