using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class PlantelConfiguration : IEntityTypeConfiguration<PlantelEntity>
    {
        public void Configure(EntityTypeBuilder<PlantelEntity> builder)
        {
            builder.ToTable("PLANTEL");
            builder.HasKey(e => e.id_plantel);

            builder.Property(p => p.id_plantel).HasColumnName("id_plantel");
            builder.Property(p => p.gid_plantel).HasColumnName("gid_plantel");
            builder.Property(p => p.cod_plantel).HasColumnName("cod_plantel");
            builder.Property(p => p.nom_plantel).HasColumnName("nom_plantel");
            builder.Property(p => p.ind_interno).HasColumnName("ind_interno");
            
            builder.SetAudithory();

            builder.HasMany(m => m.campanias).WithOne(o => o.plantel).HasForeignKey(f => f.id_plantel);
            builder.HasMany(m => m.usuario_planteles).WithOne(o => o.plantel).HasForeignKey(f => f.id_plantel);
        }
    }
}
