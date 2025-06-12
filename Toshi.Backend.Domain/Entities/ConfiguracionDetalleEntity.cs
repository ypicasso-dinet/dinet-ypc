namespace Toshi.Backend.Domain.Entities
{
    public partial class ConfiguracionDetalleEntity : BaseDomainModel
    {
        public int id_config { get; set; }
        public int id_config_det { get; set; }
        public string gid_config_det { get; set; } = string.Empty;
        public int ord_config_det { get; set; }
        public string? cod_detalle { get; set; }
        public string? nom_detalle { get; set; }
        public decimal? min_value { get; set; }
        public decimal? max_value { get; set; }
        public string? des_email { get; set; }
        public string? val_email { get; set; }
        public ConfiguracionEntity? configuracion { get; set; }
    }
}
