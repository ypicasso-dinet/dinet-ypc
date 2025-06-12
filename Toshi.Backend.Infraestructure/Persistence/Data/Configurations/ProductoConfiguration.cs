using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<ProductoEntity>
    {
        public void Configure(EntityTypeBuilder<ProductoEntity> builder)
        {
            builder.ToTable("PRODUCTO");
            builder.HasKey(e => e.id_producto);

            builder.Property(p => p.id_producto).HasColumnName("id_producto");
            builder.Property(p => p.gid_producto).HasColumnName("gid_producto");
            builder.Property(p => p.cod_producto).HasColumnName("cod_producto");
            builder.Property(p => p.nom_producto).HasColumnName("nom_producto");
            builder.Property(p => p.uni_medida).HasColumnName("uni_medida");
            builder.Property(p => p.cod_tipo).HasColumnName("cod_tipo");
            builder.Property(p => p.cod_segmento).HasColumnName("cod_segmento");
            builder.Property(p => p.min_ingreso).HasColumnName("min_ingreso");
            builder.Property(p => p.max_ingreso).HasColumnName("max_ingreso");
            builder.Property(p => p.min_salida).HasColumnName("min_salida");
            builder.Property(p => p.max_salida).HasColumnName("max_salida");
            builder.Property(p => p.min_transfer).HasColumnName("min_transfer");
            builder.Property(p => p.max_transfer).HasColumnName("max_transfer");
            
            builder.SetAudithory();

            builder.HasMany(m => m.ingreso_productos).WithOne(o => o.producto).HasForeignKey(f => f.id_producto);
            builder.HasMany(m => m.salida_productos).WithOne(o => o.producto).HasForeignKey(f => f.id_producto);
        }
    }
}
