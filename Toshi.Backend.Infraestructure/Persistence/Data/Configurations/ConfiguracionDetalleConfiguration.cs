using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class ConfiguracionDetalleConfiguration : IEntityTypeConfiguration<ConfiguracionDetalleEntity>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionDetalleEntity> builder)
        {
            builder.ToTable("CONFIGURACION_DETALLE");
            builder.HasKey(e => e.id_config_det);

            builder.Property(p => p.id_config).HasColumnName("id_config");
            builder.Property(p => p.id_config_det).HasColumnName("id_config_det");
            builder.Property(p => p.gid_config_det).HasColumnName("gid_config_det");
            builder.Property(p => p.ord_config_det).HasColumnName("ord_config_det");
            builder.Property(p => p.cod_detalle).HasColumnName("cod_detalle");
            builder.Property(p => p.nom_detalle).HasColumnName("nom_detalle");
            builder.Property(p => p.min_value).HasColumnName("min_value");
            builder.Property(p => p.max_value).HasColumnName("max_value");
            builder.Property(p => p.des_email).HasColumnName("des_email");
            builder.Property(p => p.val_email).HasColumnName("val_email");
            
            builder.SetAudithory();

            builder.HasOne(o => o.configuracion).WithMany(m => m.configuracion_detalles).HasForeignKey(f => f.id_config);
        }
    }
}
