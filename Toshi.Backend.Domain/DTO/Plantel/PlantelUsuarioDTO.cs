namespace Toshi.Backend.Domain.DTO.Plantel
{
    public class PlantelUsuarioDTO
    {
        //public string? id_plantel_usuario { get; set; }
        public string? id_usuario { get; set; }
        public string? nom_usuario { get; set; }
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        //public bool? ind_turno { get; set; }
        public string? nom_turno { get; set; }
        public string? tip_usuario { get; set; }
        public string? nom_proveedor { get; set; }
        public string? ruc_proveedor { get; set; }
        public bool? cod_estado { get; set; }
        public string? nom_estado { get; set; }
        public string? cod_rol { get; set; }
    }
}
