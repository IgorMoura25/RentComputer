using RC.Identity.API.Models;

namespace RC.Identity.API.Data.Repositories
{
    public interface IRepository
    {
        Task<JwtSecurityKey> AddSecurityKeyAsync(JwtSecurityKey model);
        Task<JwtPrivateKey> AddPrivateKeyAsync(JwtPrivateKey model);
        Task<RefreshToken> AddRefreshTokenAsync(RefreshToken model);
        Task<JwtPrivateKey?> GetCurrentPrivateKeyAsync();
        List<JasonWebKey> GetRecentKeys();
        Task<RefreshToken?> GetRefreshTokenAsync(Guid token);
    }
}
