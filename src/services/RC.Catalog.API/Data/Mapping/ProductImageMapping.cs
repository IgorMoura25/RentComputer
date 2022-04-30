using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RC.Catalog.API.Domain;

namespace RC.Catalog.API.Data.Mapping
{
    public class ProductImageMapping : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(p => new { p.Id, p.UniversalId });

            builder.Property(i => i.Id).HasColumnName("ProductImageId").UseIdentityColumn();
            builder.Property(i => i.UniversalId).HasColumnName("ProductImageGuid");
            builder.Property(p => p.Path).HasColumnType("varchar(500)").IsRequired();

            builder.ToTable("ProductImages");
        }
    }
}
