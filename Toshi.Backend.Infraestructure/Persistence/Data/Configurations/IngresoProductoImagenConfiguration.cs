using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class IngresoProductoImagenConfiguration : IEntityTypeConfiguration<IngresoProductoImagenEntity>
    {
        public void Configure(EntityTypeBuilder<IngresoProductoImagenEntity> builder)
        {
            builder.ToTable("INGRESO_PRODUCTO_IMAGEN");
            builder.HasKey(e => e.id_ingreso_producto_imagen);

            builder.Property(p => p.id_ingreso_producto_imagen).HasColumnName("id_ingreso_producto_imagen");
            builder.Property(p => p.gid_ingreso_producto_imagen).HasColumnName("gid_ingreso_producto_imagen");
            builder.Property(p => p.id_ingreso_producto).HasColumnName("id_ingreso_producto");
            builder.Property(p => p.nom_imagen).HasColumnName("nom_imagen");
            builder.Property(p => p.url_imagen).HasColumnName("url_imagen");
            builder.SetAudithory();

            builder.HasOne(o => o.ingreso_producto).WithMany(m => m.ingreso_producto_imagenes).HasForeignKey(f => f.id_ingreso_producto);
        }
    }
}
