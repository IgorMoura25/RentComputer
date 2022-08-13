using RC.Identity.API.Models;

namespace RC.Identity.API.Data.Repositories
{
    public interface ISecurityKeyRepository
    {
        Task<JwtSecurityKey> AddAsync(JwtSecurityKey model);
        Task<string?> GetCurrentPrivateKeyAsync();
        List<JasonWebKey> GetRecentKeys();
    }
}
