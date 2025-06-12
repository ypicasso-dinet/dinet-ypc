namespace Toshi.Backend.Domain.Entities
{
    public partial class UsuarioPlantelEntity : BaseDomainModel
    {
        public int id_usuario_plantel { get; set; }
        public int id_usuario { get; set; }
        public int id_plantel { get; set; }
        public PlantelEntity? plantel { get; set; }
        public UsuarioEntity? usuario { get; set; }
    }
}
