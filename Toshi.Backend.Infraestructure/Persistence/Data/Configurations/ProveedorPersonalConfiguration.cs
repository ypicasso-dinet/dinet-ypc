using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class ProveedorPersonalConfiguration : IEntityTypeConfiguration<ProveedorPersonalEntity>
    {
        public void Configure(EntityTypeBuilder<ProveedorPersonalEntity> builder)
        {
            builder.ToTable("PROVEEDOR_PERSONAL");
            builder.HasKey(e => e.id_proveedor_personal);

            builder.Property(p => p.id_proveedor_personal).HasColumnName("id_proveedor_personal");
            builder.Property(p => p.gid_proveedor_personal).HasColumnName("gid_proveedor_personal");
            builder.Property(p => p.id_proveedor).HasColumnName("id_proveedor");
            builder.Property(p => p.id_persona).HasColumnName("id_persona");
            builder.Property(p => p.num_telefono).HasColumnName("num_telefono");
            
            builder.SetAudithory();

            builder.HasOne(o => o.persona).WithMany(m => m.proveedor_personales).HasForeignKey(f => f.id_persona);
            builder.HasOne(o => o.proveedor).WithMany(m => m.proveedor_personales).HasForeignKey(f => f.id_proveedor);
        }
    }
}
