using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<PersonaEntity>
    {
        public void Configure(EntityTypeBuilder<PersonaEntity> builder)
        {
            builder.ToTable("PERSONA");
            builder.HasKey(e => e.id_persona);

            builder.Property(p => p.id_persona).HasColumnName("id_persona");
            builder.Property(p => p.gid_persona).HasColumnName("gid_persona");
            builder.Property(p => p.cod_persona).HasColumnName("cod_persona");
            builder.Property(p => p.nom_persona).HasColumnName("nom_persona");
            builder.Property(p => p.ape_paterno).HasColumnName("ape_paterno");
            builder.Property(p => p.ape_materno).HasColumnName("ape_materno");
            builder.Property(p => p.tip_documento).HasColumnName("tip_documento");
            builder.Property(p => p.num_documento).HasColumnName("num_documento");
            
            builder.SetAudithory();

            builder.HasMany(m => m.proveedor_personales).WithOne(o => o.persona).HasForeignKey(f => f.id_persona);
        }
    }
}
