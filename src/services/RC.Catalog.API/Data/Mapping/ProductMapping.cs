using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RC.Catalog.API.Domain;

namespace RC.Catalog.API.Data.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => new { p.Id, p.UniversalId });

            builder.Property(i => i.Id).HasColumnName("ProductId").UseIdentityColumn();
            builder.Property(i => i.UniversalId).HasColumnName("ProductGuid");
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(500)").IsRequired(false);
            builder.Property(p => p.Value).HasColumnType("decimal(12, 2)").IsRequired();
            builder.Property(p => p.Quantity).HasColumnType("int").IsRequired();
            builder.Property(p => p.IsActive).HasColumnType("bit").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnType("datetime").IsRequired();

            builder.HasMany(p => p.Images)
            .WithOne(p => p.Product)
            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Products");
        }
    }
}
