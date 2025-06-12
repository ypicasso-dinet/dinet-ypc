using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoScreenParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? estados { get; set; }
        public List<SalidaProductoProductoDTO>? productos { get; set; }
    }
}
