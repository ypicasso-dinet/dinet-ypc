using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class IngresoProductoConfiguration : IEntityTypeConfiguration<IngresoProductoEntity>
    {
        public void Configure(EntityTypeBuilder<IngresoProductoEntity> builder)
        {
            builder.ToTable("INGRESO_PRODUCTO");
            builder.HasKey(e => e.id_ingreso_producto);

            builder.Property(p => p.id_ingreso_producto).HasColumnName("id_ingreso_producto");
            builder.Property(p => p.gid_ingreso_producto).HasColumnName("gid_ingreso_producto");
            builder.Property(p => p.id_campania).HasColumnName("id_campania");
            builder.Property(p => p.fec_registro).HasColumnName("fec_registro");
            builder.Property(p => p.id_producto).HasColumnName("id_producto");
            builder.Property(p => p.can_ingreso).HasColumnName("can_ingreso");
            builder.Property(p => p.guia_recepcion).HasColumnName("guia_recepcion");
            builder.Property(p => p.guia_proveedor).HasColumnName("guia_proveedor");
            builder.Property(p => p.observacion).HasColumnName("observacion");
            builder.SetAudithory();

            builder.HasOne(o => o.campania).WithMany(m => m.ingreso_productos).HasForeignKey(f => f.id_campania);
            builder.HasOne(o => o.producto).WithMany(m => m.ingreso_productos).HasForeignKey(f => f.id_producto);
            builder.HasMany(m => m.ingreso_producto_imagenes).WithOne(o => o.ingreso_producto).HasForeignKey(f => f.id_ingreso_producto);
        }
    }
}
