using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class RolMenuConfiguration : IEntityTypeConfiguration<RolMenuEntity>
    {
        public void Configure(EntityTypeBuilder<RolMenuEntity> builder)
        {
            builder.ToTable("ROL_MENU");
            builder.HasKey(e => e.id_rol_menu);

            builder.Property(p => p.id_rol_menu).HasColumnName("id_rol_menu");
            builder.Property(p => p.id_rol).HasColumnName("id_rol");
            builder.Property(p => p.id_menu).HasColumnName("id_menu");
            builder.Property(p => p.ind_create).HasColumnName("ind_create");
            builder.Property(p => p.ind_read).HasColumnName("ind_read");
            builder.Property(p => p.ind_update).HasColumnName("ind_update");
            builder.Property(p => p.ind_delete).HasColumnName("ind_delete");
            builder.Property(p => p.ind_all).HasColumnName("ind_all");
            
            builder.SetAudithory();

            builder.HasOne(o => o.menu).WithMany(m => m.rol_menus).HasForeignKey(f => f.id_menu);
            builder.HasOne(o => o.rol).WithMany(m => m.rol_menus).HasForeignKey(f => f.id_rol);
        }
    }
}
