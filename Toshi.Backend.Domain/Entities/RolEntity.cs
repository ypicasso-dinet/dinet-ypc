namespace Toshi.Backend.Domain.Entities
{
    public partial class RolEntity : BaseDomainModel
    {
        public int id_rol { get; set; }
        public string gid_rol { get; set; } = string.Empty;
        public string cod_rol { get; set; } = string.Empty;
        public string nom_rol { get; set; } = string.Empty;
        public List<RolMenuEntity>? rol_menus { get; set; }
        public List<UsuarioEntity>? usuarios { get; set; }
    }
}
