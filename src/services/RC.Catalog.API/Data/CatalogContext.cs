using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RC.Catalog.API.Configurations;
using RC.Catalog.API.Domain;
using RC.Core.Data;
using RC.Core.Messages;
using RC.MessageBus.Mediator;

namespace RC.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        private readonly IOptions<DataBaseSettings> _databaseSettings;
        private readonly MediatREventList _eventList;

        public CatalogContext(DbContextOptions<CatalogContext> options, IOptions<DataBaseSettings> databaseSettings, MediatREventList eventList) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _eventList = eventList;

            _databaseSettings = databaseSettings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public bool BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CommitAsync()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _eventList.PublishEventsAsync();
            }

            return success;
        }

        public Task<bool> RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
