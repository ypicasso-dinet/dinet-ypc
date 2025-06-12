namespace Toshi.Backend.Domain.DTO.Plantel
{
    public class PlantelDTO
    {
        public string? id_plantel { get; set; }
        public string? cod_plantel { get; set; }
        public string? nom_plantel { get; set; }
        public string? nom_estado { get; set; }
        public bool? ind_interno { get; set; }

        public List<PlantelRolDTO>? roles { get; set; }
    }
}
