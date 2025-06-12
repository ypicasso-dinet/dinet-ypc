using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class EnvioDiarioConfiguration : IEntityTypeConfiguration<EnvioDiarioEntity>
    {
        public void Configure(EntityTypeBuilder<EnvioDiarioEntity> builder)
        {
            builder.ToTable("ENVIO_DIARIO");
            builder.HasKey(e => e.id_envio_diario);

            builder.Property(p => p.id_envio_diario).HasColumnName("id_envio_diario");
            builder.Property(p => p.gid_envio_diario).HasColumnName("gid_envio_diario");
            builder.Property(p => p.id_campania).HasColumnName("id_campania");
            builder.Property(p => p.fec_envio).HasColumnName("fec_envio");
            builder.Property(p => p.ind_enviado).HasColumnName("ind_enviado");
            
            builder.SetAudithory();

            builder.HasOne(o => o.campania).WithMany(m => m.envio_diarios).HasForeignKey(f => f.id_campania);
        }
    }
}
