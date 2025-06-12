using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<TokenEntity>
    {
        public void Configure(EntityTypeBuilder<TokenEntity> builder)
        {
            builder.ToTable("TOKEN");
            builder.HasKey(e => e.id_token);

            builder.Property(p => p.id_token).HasColumnName("id_token");
            builder.Property(p => p.token).HasColumnName("token");
            
            builder.SetAudithory();

        }
    }
}
