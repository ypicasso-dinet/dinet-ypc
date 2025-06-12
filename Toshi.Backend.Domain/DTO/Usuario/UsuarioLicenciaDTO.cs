namespace Toshi.Backend.Domain.DTO.Usuario
{
    public class UsuarioLicenciaDTO
    {
        public string? id_licencia { get; set; }
        public string? tip_licencia { get; set; }
        public string? fec_desde { get; set; }
        public string? fec_hasta { get; set; }
        public string? obs_licencia { get; set; }
        public string? usu_insert { get; set; }
        public string? fec_insert { get; set; }
        public int? num_dias { get; set; }
    }
}
