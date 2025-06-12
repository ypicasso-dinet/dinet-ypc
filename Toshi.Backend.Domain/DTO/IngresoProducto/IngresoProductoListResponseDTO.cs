using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoListResponseDTO
    {
        public List<IngresoProductoTab1DTO>? info_tab1 { get; set; }
        public List<IngresoProductoTab2DTO>? info_tab2 { get; set; }
    }
}
