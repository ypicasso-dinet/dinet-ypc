using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class IngresoPolloImagenConfiguration : IEntityTypeConfiguration<IngresoPolloImagenEntity>
    {
        public void Configure(EntityTypeBuilder<IngresoPolloImagenEntity> builder)
        {
            builder.ToTable("INGRESO_POLLO_IMAGEN");
            builder.HasKey(e => e.id_ingreso_pollo_imagen);

            builder.Property(p => p.id_ingreso_pollo_imagen).HasColumnName("id_ingreso_pollo_imagen");
            builder.Property(p => p.gid_ingreso_pollo_imagen).HasColumnName("gid_ingreso_pollo_imagen");
            builder.Property(p => p.id_ingreso_pollo).HasColumnName("id_ingreso_pollo");
            builder.Property(p => p.nom_imagen).HasColumnName("nom_imagen");
            builder.Property(p => p.url_imagen).HasColumnName("url_imagen");
            
            builder.SetAudithory();

            builder.HasOne(o => o.ingreso_pollo).WithMany(m => m.ingreso_pollo_imagenes).HasForeignKey(f => f.id_ingreso_pollo);
        }
    }
}
