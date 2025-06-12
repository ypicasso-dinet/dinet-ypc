using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<ProveedorEntity>
    {
        public void Configure(EntityTypeBuilder<ProveedorEntity> builder)
        {
            builder.ToTable("PROVEEDOR");
            builder.HasKey(e => e.id_proveedor);

            builder.Property(p => p.id_proveedor).HasColumnName("id_proveedor");
            builder.Property(p => p.gid_proveedor).HasColumnName("gid_proveedor");
            builder.Property(p => p.nom_proveedor).HasColumnName("nom_proveedor");
            builder.Property(p => p.ruc_proveedor).HasColumnName("ruc_proveedor");
            
            builder.SetAudithory();

            builder.HasMany(m => m.proveedor_personales).WithOne(o => o.proveedor).HasForeignKey(f => f.id_proveedor);
            builder.HasMany(m => m.usuarios).WithOne(o => o.proveedor).HasForeignKey(f => f.id_proveedor);
        }
    }
}
