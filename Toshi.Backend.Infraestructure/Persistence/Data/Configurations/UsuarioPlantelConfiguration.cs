using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class UsuarioPlantelConfiguration : IEntityTypeConfiguration<UsuarioPlantelEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioPlantelEntity> builder)
        {
            builder.ToTable("USUARIO_PLANTEL");
            builder.HasKey(e => e.id_usuario_plantel);

            builder.Property(p => p.id_usuario_plantel).HasColumnName("id_usuario_plantel");
            builder.Property(p => p.id_usuario).HasColumnName("id_usuario");
            builder.Property(p => p.id_plantel).HasColumnName("id_plantel");
            
            builder.SetAudithory();

            builder.HasOne(o => o.plantel).WithMany(m => m.usuario_planteles).HasForeignKey(f => f.id_plantel);
            builder.HasOne(o => o.usuario).WithMany(m => m.usuario_planteles).HasForeignKey(f => f.id_usuario);
        }
    }
}
