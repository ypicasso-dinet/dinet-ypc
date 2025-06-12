namespace Toshi.Backend.Domain.DTO.Proveedor
{
    public class ProveedorRolDTO
    {
        public string? id_rol { get; set; }
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
        public List<ProveedorRolPersonalDTO>? personal { get; set; }
    }
}
