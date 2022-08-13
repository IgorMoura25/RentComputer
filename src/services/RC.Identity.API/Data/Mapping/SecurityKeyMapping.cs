using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RC.Identity.API.Models;

namespace RC.Catalog.API.Data.Mapping
{
    public class SecurityKeyMapping : IEntityTypeConfiguration<JwtSecurityKey>
    {
        public void Configure(EntityTypeBuilder<JwtSecurityKey> builder)
        {
            builder.HasKey(p => new { p.Id });

            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.PrivateKey).HasColumnType("varchar(MAX)");
            builder.Property(p => p.PublicParameters).HasColumnType("varchar(MAX)");
            builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();

            builder.ToTable("SecurityKeys");
        }
    }
}
