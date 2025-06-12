using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class IngresoPolloConfiguration : IEntityTypeConfiguration<IngresoPolloEntity>
    {
        public void Configure(EntityTypeBuilder<IngresoPolloEntity> builder)
        {
            builder.ToTable("INGRESO_POLLO");
            builder.HasKey(e => e.id_ingreso_pollo);

            builder.Property(p => p.id_ingreso_pollo).HasColumnName("id_ingreso_pollo");
            builder.Property(p => p.gid_ingreso_pollo).HasColumnName("gid_ingreso_pollo");
            builder.Property(p => p.id_campania).HasColumnName("id_campania");
            builder.Property(p => p.fec_registro).HasColumnName("fec_registro");
            builder.Property(p => p.num_galpon).HasColumnName("num_galpon");
            builder.Property(p => p.cod_genero).HasColumnName("cod_genero");
            builder.Property(p => p.can_ingreso).HasColumnName("can_ingreso");
            builder.Property(p => p.lot_procedencia).HasColumnName("lot_procedencia");
            builder.Property(p => p.nom_procedencia).HasColumnName("nom_procedencia");
            builder.Property(p => p.num_guia).HasColumnName("num_guia");
            builder.Property(p => p.cod_edad).HasColumnName("cod_edad");
            builder.Property(p => p.cod_lote).HasColumnName("cod_lote");
            builder.Property(p => p.cod_linea).HasColumnName("cod_linea");
            builder.Property(p => p.val_peso).HasColumnName("val_peso");
            builder.Property(p => p.can_muertos).HasColumnName("can_muertos");
            builder.Property(p => p.can_real).HasColumnName("can_real");
            builder.Property(p => p.num_vehiculo).HasColumnName("num_vehiculo");
            builder.Property(p => p.temp_cabina).HasColumnName("temp_cabina");
            builder.Property(p => p.hum_cabina).HasColumnName("hum_cabina");
            
            builder.SetAudithory();

            builder.HasOne(o => o.campania).WithMany(m => m.ingreso_pollos).HasForeignKey(f => f.id_campania);
            builder.HasMany(m => m.ingreso_pollo_imagenes).WithOne(o => o.ingreso_pollo).HasForeignKey(f => f.id_ingreso_pollo);
        }
    }
}
