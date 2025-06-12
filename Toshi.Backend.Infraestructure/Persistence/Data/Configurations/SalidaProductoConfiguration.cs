using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class SalidaProductoConfiguration : IEntityTypeConfiguration<SalidaProductoEntity>
    {
        public void Configure(EntityTypeBuilder<SalidaProductoEntity> builder)
        {
            builder.ToTable("SALIDA_PRODUCTO");
            builder.HasKey(e => e.id_salida_producto);

            builder.Property(p => p.id_salida_producto).HasColumnName("id_salida_producto");
            builder.Property(p => p.gid_salida_producto).HasColumnName("gid_salida_producto");
            builder.Property(p => p.id_usuario).HasColumnName("id_usuario");
            builder.Property(p => p.id_campania).HasColumnName("id_campania");
            builder.Property(p => p.fec_registro).HasColumnName("fec_registro");
            builder.Property(p => p.id_producto).HasColumnName("id_producto");
            builder.Property(p => p.can_salida).HasColumnName("can_salida");
            builder.Property(p => p.tip_producto).HasColumnName("tip_producto");
            builder.Property(p => p.uni_producto).HasColumnName("uni_producto");
            builder.Property(p => p.observacion).HasColumnName("observacion");
            
            builder.SetAudithory();

            builder.HasOne(o => o.campania).WithMany(m => m.salida_productos).HasForeignKey(f => f.id_campania);
            builder.HasOne(o => o.producto).WithMany(m => m.salida_productos).HasForeignKey(f => f.id_producto);
            builder.HasOne(o => o.usuario).WithMany(m => m.salida_productos).HasForeignKey(f => f.id_usuario);
            builder.HasMany(m => m.salida_producto_imagenes).WithOne(o => o.salida_producto).HasForeignKey(f => f.id_salida_producto);
        }
    }
}
