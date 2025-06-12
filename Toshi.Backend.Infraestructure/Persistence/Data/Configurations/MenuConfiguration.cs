using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.ToTable("MENU");
            builder.HasKey(e => e.id_menu);

            builder.Property(p => p.id_menu).HasColumnName("id_menu");
            builder.Property(p => p.id_padre).HasColumnName("id_padre");
            builder.Property(p => p.cod_menu).HasColumnName("cod_menu");
            builder.Property(p => p.tit_menu).HasColumnName("tit_menu");
            builder.Property(p => p.ico_menu).HasColumnName("ico_menu");
            builder.Property(p => p.url_menu).HasColumnName("url_menu");
            builder.Property(p => p.ord_menu).HasColumnName("ord_menu");
            builder.Property(p => p.ind_entorno).HasColumnName("ind_entorno");
            
            builder.SetAudithory();

            builder.HasMany(m => m.rol_menus).WithOne(o => o.menu).HasForeignKey(f => f.id_menu);
        }
    }
}
