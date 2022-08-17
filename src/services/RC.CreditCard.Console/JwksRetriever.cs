using Microsoft.IdentityModel.Tokens;

namespace RC.CreditCard.Console
{
    public static class JwksRetriever
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public static async Task<JsonWebKey?> GetPublicKey()
        {
            var publicKey = await _httpClient.GetStringAsync("https://localhost:7241/jwks");
            var key = JsonWebKeySet.Create(publicKey);

            return key.Keys.FirstOrDefault();
        }
    }
}
