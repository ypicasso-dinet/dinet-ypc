namespace Toshi.Backend.Domain.Entities
{
    public partial class EnvioDiarioEntity : BaseDomainModel
    {
        public int id_envio_diario { get; set; }
        public string gid_envio_diario { get; set; } = string.Empty;
        public int id_campania { get; set; }
        public DateTime fec_envio { get; set; }
        public bool ind_enviado { get; set; }
        public CampaniaEntity? campania { get; set; }
    }
}
