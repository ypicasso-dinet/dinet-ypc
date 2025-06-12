namespace Toshi.Backend.Domain.DTO.Proveedor
{
    public class ProveedorDTO
    {
        public string? id_proveedor { get; set; }
        public string? ruc_proveedor { get; set; }
        public string? nom_proveedor { get; set; }
        public string? nom_estado { get; set; }

        public bool allow_update { get; set; }

        public List<ProveedorRolDTO>? roles { get; set; }
        public List<ProveedorRolPersonalDTO>? personal { get; set; }
    }
}
