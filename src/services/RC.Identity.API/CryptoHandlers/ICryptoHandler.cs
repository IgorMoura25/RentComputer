using System.Security.Claims;

namespace RC.Identity.API.CryptoHandlers
{
    public interface ICryptoHandler
    {
        Task<string> CreateJwtTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires);
        Task<string> CreateJwtTokenFromDistributedCacheAsync(string issuer, ClaimsIdentity? subject, DateTime expires);
        Task CreateKeysAsync();
    }
}
