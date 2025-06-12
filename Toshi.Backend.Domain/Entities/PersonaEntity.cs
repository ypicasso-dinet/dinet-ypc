namespace Toshi.Backend.Domain.Entities
{
    public partial class PersonaEntity : BaseDomainModel
    {
        public int id_persona { get; set; }
        public string gid_persona { get; set; } = string.Empty;
        public string cod_persona { get; set; } = string.Empty;
        public string nom_persona { get; set; } = string.Empty;
        public string ape_paterno { get; set; } = string.Empty;
        public string? ape_materno { get; set; }
        public string tip_documento { get; set; } = string.Empty;
        public string num_documento { get; set; } = string.Empty;
        public List<ProveedorPersonalEntity>? proveedor_personales { get; set; }
    }
}
