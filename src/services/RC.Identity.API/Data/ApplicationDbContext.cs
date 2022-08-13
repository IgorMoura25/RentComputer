using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RC.Identity.API.Configurations;
using RC.Identity.API.Models;

namespace RC.Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IOptions<DataBaseSettings> _databaseSettings;
        public DbSet<JwtSecurityKey> SecurityKeys { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<DataBaseSettings> databaseSettings) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _databaseSettings = databaseSettings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
