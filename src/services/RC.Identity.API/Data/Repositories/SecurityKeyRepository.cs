using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RC.Identity.API.Models;

namespace RC.Identity.API.Data.Repositories
{
    public class SecurityKeyRepository : ISecurityKeyRepository
    {
        private readonly ApplicationDbContext _context;

        public SecurityKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JwtSecurityKey> AddAsync(JwtSecurityKey model)
        {
            await _context.SecurityKeys.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<JwtPrivateKey> AddPrivateKeyAsync(JwtPrivateKey model)
        {
            var previousKey = await GetCurrentPrivateKeyAsync();

            if (previousKey != null)
            {
                _context.PrivateKey.Remove(previousKey);
            }

            await _context.PrivateKey.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<JwtPrivateKey?> GetCurrentPrivateKeyAsync()
        {
            return await _context.PrivateKey.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public List<JasonWebKey> GetRecentKeys()
        {
            var securityKeys = _context.SecurityKeys.AsNoTracking().OrderByDescending(x => x.Id).Take(2).ToList();

            var jasonWebKeys = new List<JasonWebKey>();

            foreach (var key in securityKeys)
            {
                if (!string.IsNullOrEmpty(key.PublicParameters))
                {
                    var jasonWebKey = JsonSerializer.Deserialize<JasonWebKey>(key.PublicParameters);

                    if (jasonWebKey != null)
                    {
                        jasonWebKeys.Add(jasonWebKey);
                    }
                }
            }

            return jasonWebKeys;
        }
    }
}
