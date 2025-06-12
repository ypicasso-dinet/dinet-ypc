using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class EstandarConfiguration : IEntityTypeConfiguration<EstandarEntity>
    {
        public void Configure(EntityTypeBuilder<EstandarEntity> builder)
        {
            builder.ToTable("ESTANDAR");
            builder.HasKey(e => e.id_estandar);

            builder.Property(p => p.id_estandar).HasColumnName("id_estandar");
            builder.Property(p => p.gid_estandar).HasColumnName("gid_estandar");
            builder.Property(p => p.cod_carde).HasColumnName("cod_carde");
            builder.Property(p => p.cod_lote).HasColumnName("cod_lote");
            builder.Property(p => p.val_edad).HasColumnName("val_edad");
            builder.Property(p => p.cod_sexo).HasColumnName("cod_sexo");
            builder.Property(p => p.val_estandar).HasColumnName("val_estandar");
            builder.Property(p => p.val_peso).HasColumnName("val_peso");
            builder.Property(p => p.val_consumo).HasColumnName("val_consumo");
            builder.Property(p => p.val_mortalidad).HasColumnName("val_mortalidad");
            builder.Property(p => p.val_conversion).HasColumnName("val_conversion");
            builder.Property(p => p.val_eficiencia).HasColumnName("val_eficiencia");
            
            builder.SetAudithory();

        }
    }
}
