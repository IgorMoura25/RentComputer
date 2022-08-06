using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RC.Identity.API.Configurations;

namespace RC.Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IOptions<DataBaseSettings> _databaseSettings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<DataBaseSettings> databaseSettings) : base(options)
        {
            _databaseSettings = databaseSettings;
        }
    }
}
