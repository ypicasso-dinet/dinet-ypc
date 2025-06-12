namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoPlantelDTO
    {
        public string? id_plantel { get; set; }
        public string? nom_plantel { get; set; }
        public List<SalidaProductoCampaniaDTO>? campanias { get; set; }
    }
}
