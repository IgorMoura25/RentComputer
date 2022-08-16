using RC.Identity.API.Models;

namespace RC.Identity.API.Data.Repositories
{
    public interface ISecurityKeyRepository
    {
        Task<JwtSecurityKey> AddAsync(JwtSecurityKey model);
        Task<JwtPrivateKey> AddPrivateKeyAsync(JwtPrivateKey model);
        Task<JwtPrivateKey?> GetCurrentPrivateKeyAsync();
        List<JasonWebKey> GetRecentKeys();
    }
}
