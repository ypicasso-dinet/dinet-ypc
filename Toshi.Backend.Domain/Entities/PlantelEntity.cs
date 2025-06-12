namespace Toshi.Backend.Domain.Entities
{
    public partial class PlantelEntity : BaseDomainModel
    {
        public int id_plantel { get; set; }
        public string gid_plantel { get; set; } = string.Empty;
        public string cod_plantel { get; set; } = string.Empty;
        public string nom_plantel { get; set; } = string.Empty;
        public bool ind_interno { get; set; }
        public List<CampaniaEntity>? campanias { get; set; }
        public List<UsuarioPlantelEntity>? usuario_planteles { get; set; }
    }
}
