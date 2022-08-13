using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace RC.WebAPI.Core.Auth.Extensions
{
    public static class JwksExtension
    {
        public static void SetJwksRetrieverOptions(this JwtBearerOptions options, string? retrievalUrl)
        {
            if (retrievalUrl == null) throw new ArgumentNullException(nameof(retrievalUrl));

            options.RequireHttpsMetadata = true;

            // Salva o token buscado, em um cache
            options.SaveToken = true;

            var httpClient = new HttpClient(options.BackchannelHttpHandler ?? new HttpClientHandler())
            {
                Timeout = options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10 // 10 MB 
            };

            // Define a URI que irá buscar o Jason Web Key Set (JWKS)
            var jwksUri = new Uri(retrievalUrl);

            // O ConfigurationManager se encarrega de buscar, cachear e atualizar o JWKS
            // Sendo assim, instanciamos uma nova classe e passamos o NOSSO buscador customizado JwksRetriever
            options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                retrievalUrl,
                new JwksRetriever(),
                new HttpDocumentRetriever(httpClient) { RequireHttps = options.RequireHttpsMetadata });

            // Não é necessário validar audiência, pois aonde o token for válido, ele é válido
            options.TokenValidationParameters.ValidateAudience = false;

            // Validar o tempo de expiração do token através da claim Iat (issued at)
            options.TokenValidationParameters.ValidateLifetime = true;
            options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;

            // O emissor válido (Url do authorization server)
            options.TokenValidationParameters.ValidIssuer = $"{jwksUri.Scheme}://{jwksUri.Authority}";
        }
    }
}