using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toshi.Backend.Domain.Entities;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public class SolContraseniaConfiguration : IEntityTypeConfiguration<SolContraseniaEntity>
    {
        public void Configure(EntityTypeBuilder<SolContraseniaEntity> builder)
        {
            builder.ToTable("SOL_CONTRASENIA");
            builder.HasKey(e => e.id_sol_contrasenia);

            builder.Property(p => p.id_sol_contrasenia).HasColumnName("id_sol_contrasenia");
            builder.Property(p => p.id_usuario).HasColumnName("id_usuario");
            builder.Property(p => p.ind_proceso).HasColumnName("ind_proceso");
            builder.Property(p => p.new_contrasenia).HasColumnName("new_contrasenia");
            
            builder.SetAudithory();

        }
    }
}
