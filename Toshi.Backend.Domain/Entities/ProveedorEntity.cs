namespace Toshi.Backend.Domain.Entities
{
    public partial class ProveedorEntity : BaseDomainModel
    {
        public int id_proveedor { get; set; }
        public string gid_proveedor { get; set; } = string.Empty;
        public string nom_proveedor { get; set; } = string.Empty;
        public string ruc_proveedor { get; set; } = string.Empty;
        public List<ProveedorPersonalEntity>? proveedor_personales { get; set; }
        public List<UsuarioEntity>? usuarios { get; set; }
    }
}
