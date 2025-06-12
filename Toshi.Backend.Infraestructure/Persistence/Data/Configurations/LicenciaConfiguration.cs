using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class LicenciaConfiguration : IEntityTypeConfiguration<LicenciaEntity>
    {
        public void Configure(EntityTypeBuilder<LicenciaEntity> builder)
        {
            builder.ToTable("LICENCIA");
            builder.HasKey(e => e.id_licencia);

            builder.Property(p => p.id_licencia).HasColumnName("id_licencia");
            builder.Property(p => p.gid_licencia).HasColumnName("gid_licencia");
            builder.Property(p => p.id_usuario).HasColumnName("id_usuario");
            builder.Property(p => p.tip_licencia).HasColumnName("tip_licencia");
            builder.Property(p => p.fec_desde).HasColumnName("fec_desde");
            builder.Property(p => p.fec_hasta).HasColumnName("fec_hasta");
            builder.Property(p => p.obs_licencia).HasColumnName("obs_licencia");
            
            builder.SetAudithory();

            builder.HasOne(o => o.usuario).WithMany(m => m.licencias).HasForeignKey(f => f.id_usuario);
        }
    }
}
