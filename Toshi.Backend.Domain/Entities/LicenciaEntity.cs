namespace Toshi.Backend.Domain.Entities
{
    public partial class LicenciaEntity : BaseDomainModel
    {
        public int id_licencia { get; set; }
        public string gid_licencia { get; set; } = string.Empty;
        public int id_usuario { get; set; }
        public string tip_licencia { get; set; } = string.Empty;
        public DateTime fec_desde { get; set; }
        public DateTime fec_hasta { get; set; }
        public string? obs_licencia { get; set; }
        public UsuarioEntity? usuario { get; set; }
    }
}
