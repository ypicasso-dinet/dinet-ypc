using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class SalidaProductoImagenConfiguration : IEntityTypeConfiguration<SalidaProductoImagenEntity>
    {
        public void Configure(EntityTypeBuilder<SalidaProductoImagenEntity> builder)
        {
            builder.ToTable("SALIDA_PRODUCTO_IMAGEN");
            builder.HasKey(e => e.id_salida_producto_imagen);

            builder.Property(p => p.id_salida_producto_imagen).HasColumnName("id_salida_producto_imagen");
            builder.Property(p => p.gid_salida_producto_imagen).HasColumnName("gid_salida_producto_imagen");
            builder.Property(p => p.id_salida_producto).HasColumnName("id_salida_producto");
            builder.Property(p => p.nom_imagen).HasColumnName("nom_imagen");
            builder.Property(p => p.url_imagen).HasColumnName("url_imagen");
            
            builder.SetAudithory();

            builder.HasOne(o => o.salida_producto).WithMany(m => m.salida_producto_imagenes).HasForeignKey(f => f.id_salida_producto);
        }
    }
}
