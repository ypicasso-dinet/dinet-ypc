namespace Toshi.Backend.Domain.DTO.Configuracion
{
    public class ConfiguracionDetalleDTO
    {
        public string? id_detalle { get; set; }
        public int? ord_detalle { get; set; }
        public string? cod_detalle { get; set; }
        public string? nom_detalle { get; set; }
        public decimal? min_value { get; set; }
        public decimal? max_value { get; set; }
        public string? des_email { get; set; }
        public string? val_email { get; set; }
        public bool? cod_estado { get; set; }
        public string? nom_estado { get; set; }
    }
}
