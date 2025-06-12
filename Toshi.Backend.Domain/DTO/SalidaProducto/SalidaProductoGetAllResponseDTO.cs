namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoGetAllResponseDTO
    {
        public List<SalidaProductoItemDTO>? salidas { get; set; }
        public List<SalidaProductoTotalesDTO>? totales { get; set; }
    }
}
