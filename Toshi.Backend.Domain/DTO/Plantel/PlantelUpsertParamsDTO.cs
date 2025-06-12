namespace Toshi.Backend.Domain.DTO.Plantel
{
    public class PlantelUpsertParamsDTO
    {
        public List<PlantelRolDTO>? roles { get; set; }
        public List<PlantelUsuarioDTO>? usuarios { get; set; }
    }
}
