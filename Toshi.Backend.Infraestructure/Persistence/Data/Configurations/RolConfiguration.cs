using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<RolEntity>
    {
        public void Configure(EntityTypeBuilder<RolEntity> builder)
        {
            builder.ToTable("ROL");
            builder.HasKey(e => e.id_rol);

            builder.Property(p => p.id_rol).HasColumnName("id_rol");
            builder.Property(p => p.gid_rol).HasColumnName("gid_rol");
            builder.Property(p => p.cod_rol).HasColumnName("cod_rol");
            builder.Property(p => p.nom_rol).HasColumnName("nom_rol");
            
            builder.SetAudithory();

            builder.HasMany(m => m.rol_menus).WithOne(o => o.rol).HasForeignKey(f => f.id_rol);
            builder.HasMany(m => m.usuarios).WithOne(o => o.rol).HasForeignKey(f => f.id_rol);
        }
    }
}
