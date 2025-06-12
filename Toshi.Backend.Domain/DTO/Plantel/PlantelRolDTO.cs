namespace Toshi.Backend.Domain.DTO.Plantel
{
    public class PlantelRolDTO
    {
        public string? id_rol { get; set; }
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
        public bool es_galponero { get { return string.Equals("GALP", cod_rol, StringComparison.OrdinalIgnoreCase); } }
        public List<PlantelUsuarioDTO>? usuarios { get; set; }
    }
}
