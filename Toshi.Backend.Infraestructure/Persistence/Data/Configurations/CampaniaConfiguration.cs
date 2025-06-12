using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class CampaniaConfiguration : IEntityTypeConfiguration<CampaniaEntity>
    {
        public void Configure(EntityTypeBuilder<CampaniaEntity> builder)
        {
            builder.ToTable("CAMPANIA");
            builder.HasKey(e => e.id_campania);

            builder.Property(p => p.id_campania).HasColumnName("id_campania");
            builder.Property(p => p.gid_campania).HasColumnName("gid_campania");
            builder.Property(p => p.id_plantel).HasColumnName("id_plantel");
            builder.Property(p => p.num_anio).HasColumnName("num_anio");
            builder.Property(p => p.num_campania).HasColumnName("num_campania");
            builder.Property(p => p.cod_campania).HasColumnName("cod_campania");
            builder.Property(p => p.fec_limpieza).HasColumnName("fec_limpieza");
            builder.Property(p => p.fec_ingreso).HasColumnName("fec_ingreso");
            builder.Property(p => p.fec_venta).HasColumnName("fec_venta");
            builder.Property(p => p.fec_cierre).HasColumnName("fec_cierre");
            builder.Property(p => p.can_ingreso).HasColumnName("can_ingreso");
            builder.Property(p => p.can_mortalidad).HasColumnName("can_mortalidad");
            builder.Property(p => p.can_venta).HasColumnName("can_venta");
            builder.Property(p => p.ind_proceso).HasColumnName("ind_proceso");
            
            builder.SetAudithory();

            builder.HasOne(o => o.plantel).WithMany(m => m.campanias).HasForeignKey(f => f.id_plantel);
            builder.HasMany(m => m.envio_diarios).WithOne(o => o.campania).HasForeignKey(f => f.id_campania);
            builder.HasMany(m => m.ingreso_pollos).WithOne(o => o.campania).HasForeignKey(f => f.id_campania);
            builder.HasMany(m => m.ingreso_productos).WithOne(o => o.campania).HasForeignKey(f => f.id_campania);
            builder.HasMany(m => m.salida_productos).WithOne(o => o.campania).HasForeignKey(f => f.id_campania);
        }
    }
}
