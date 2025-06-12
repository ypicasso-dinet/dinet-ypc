namespace Toshi.Backend.Domain.Entities
{
    public partial class ConfiguracionEntity : BaseDomainModel
    {
        public int id_config { get; set; }
        public string gid_config { get; set; } = string.Empty;
        public string cod_config { get; set; } = string.Empty;
        public string nom_config { get; set; } = string.Empty;
        public string tip_config { get; set; } = string.Empty;
        public List<ConfiguracionDetalleEntity>? configuracion_detalles { get; set; }
    }
}
