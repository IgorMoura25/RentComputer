using System.Security.Claims;
using RC.Identity.API.Models;

namespace RC.Identity.API.CryptoHandlers
{
    public interface ICryptoHandler
    {
        Task<string> CreateJwtTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires);
        Task<string> CreateJwtTokenFromDistributedCacheAsync(string issuer, ClaimsIdentity? subject, DateTime expires);
        Task CreateKeysAsync();
        Task<bool> ValidateJweCreditCardAsync(string jwe);
        Task<RefreshToken> CreateRefreshTokenAsync(string email);
        Task<RefreshToken?> GetRefreshToken(Guid refreshToken);
        Task<string> CreateJwtRefreshTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires);
        Task<string?> GetSubjectFromJwtRefreshToken(string refreshToken);
    }
}
