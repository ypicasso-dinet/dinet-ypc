using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class ConfiguracionConfiguration : IEntityTypeConfiguration<ConfiguracionEntity>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionEntity> builder)
        {
            builder.ToTable("CONFIGURACION");
            builder.HasKey(e => e.id_config);

            builder.Property(p => p.id_config).HasColumnName("id_config");
            builder.Property(p => p.gid_config).HasColumnName("gid_config");
            builder.Property(p => p.cod_config).HasColumnName("cod_config");
            builder.Property(p => p.nom_config).HasColumnName("nom_config");
            builder.Property(p => p.tip_config).HasColumnName("tip_config");
            
            builder.SetAudithory();

            builder.HasMany(m => m.configuracion_detalles).WithOne(o => o.configuracion).HasForeignKey(f => f.id_config);
        }
    }
}
