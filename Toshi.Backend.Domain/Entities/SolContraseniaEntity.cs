namespace Toshi.Backend.Domain.Entities
{
    public partial class SolContraseniaEntity : BaseDomainModel
    {
        public int id_sol_contrasenia { get; set; }
        public int id_usuario { get; set; }
        public int ind_proceso { get; set; }
        public string? new_contrasenia { get; set; }
    }
}
