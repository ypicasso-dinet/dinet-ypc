namespace Toshi.Backend.Domain.Entities
{
    public partial class ProveedorPersonalEntity : BaseDomainModel
    {
        public int id_proveedor_personal { get; set; }
        public string gid_proveedor_personal { get; set; } = string.Empty;
        public int id_proveedor { get; set; }
        public int id_persona { get; set; }
        public string? num_telefono { get; set; }
        public PersonaEntity? persona { get; set; }
        public ProveedorEntity? proveedor { get; set; }
    }
}
