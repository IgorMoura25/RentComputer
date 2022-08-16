using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RC.Identity.API.Models;

namespace RC.Catalog.API.Data.Mapping
{
    public class PrivateKeyMapping : IEntityTypeConfiguration<JwtPrivateKey>
    {
        public void Configure(EntityTypeBuilder<JwtPrivateKey> builder)
        {
            builder.HasKey(p => new { p.Id });

            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.PrivateKey).HasColumnType("varchar(MAX)");
            builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();

            builder.ToTable("PrivateKey");
        }
    }
}
