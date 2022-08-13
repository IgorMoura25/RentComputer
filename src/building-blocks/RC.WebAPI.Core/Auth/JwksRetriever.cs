using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace RC.WebAPI.Core.Auth
{
    public class JwksRetriever : IConfigurationRetriever<OpenIdConnectConfiguration>
    {
        public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (retriever == null)
                throw new ArgumentNullException(nameof(retriever));

            IdentityModelEventSource.ShowPII = true;

            var doc = await retriever.GetDocumentAsync(address, cancel);

            LogHelper.LogVerbose("IDX21811: Deserializing the string: '{0}' obtained from metadata endpoint into openIdConnectConfiguration object.", doc);

            var jwks = new JsonWebKeySet(doc);

            var openIdConnectConfiguration = new OpenIdConnectConfiguration()
            {
                JsonWebKeySet = jwks,
                JwksUri = address,
            };

            foreach (var securityKey in jwks.GetSigningKeys())
                openIdConnectConfiguration.SigningKeys.Add(securityKey);

            return openIdConnectConfiguration;
        }
    }
}
