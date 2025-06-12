using Toshi.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Toshi.Backend.Infraestructure.Persistence.Data.Configurations
{
    public static class ConfigurationExtensions
    {
        public static void SetAudithory<T>(this EntityTypeBuilder<T> builder) where T : BaseDomainModel
        {
            builder.Property(p => p.cod_estado).HasColumnName("cod_estado");
            builder.Property(p => p.usu_insert).HasColumnName("usu_insert");
            builder.Property(p => p.fec_insert).HasColumnName("fec_insert");
            builder.Property(p => p.usu_update).HasColumnName("usu_update");
            builder.Property(p => p.fec_update).HasColumnName("fec_update");
        }
    }
}
