namespace Toshi.Backend.Domain.DTO.Configuracion
{
    public class ConfiguracionDTO
    {
        public string? id_config { get; set; }
        public string? nom_config { get; set; }
        public string? tip_config { get; set; }
        public List<ConfiguracionDetalleDTO>? detalle { get; set; }
    }
}
