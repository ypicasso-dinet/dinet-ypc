namespace Toshi.Backend.Domain.DTO.Proveedor
{
    public class ProveedorRolPersonalDTO
    {
        public string? id_personal { get; set; }
        public string? cod_personal { get; set; }
        public string? nom_personal { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? fec_nacimiento { get; set; }
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        public string? per_email { get; set; }
        public string? num_telefono { get; set; }
        public string? id_rol { get; set; }
        public string? id_plantel { get; set; }
        public string? id_proveedor { get; set; }
        public bool? ind_turno { get; set; }
        public string? nom_turno { get; set; }
        public string? nom_estado { get; set; }

        public bool allow_update { get; set; }
        public bool allow_delete { get; set; }
    }
}
