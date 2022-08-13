using RC.Identity.API.CryptoHandlers;
using RC.WebAPI.Core.Auth;

namespace RC.Identity.API.Configurations
{
    public static class JwtSigningConfiguration
    {
        public static IServiceCollection AddJwtSigningCryptographyConfiguration(this IServiceCollection services, JwtSigningCryptography cryptography)
        {
            switch (cryptography)
            {
                case JwtSigningCryptography.Rsa:
                default:
                    services.AddScoped<ICryptoHandler, RsaCryptoHandler>();
                    break;
            }

            return services;
        }
    }
}
