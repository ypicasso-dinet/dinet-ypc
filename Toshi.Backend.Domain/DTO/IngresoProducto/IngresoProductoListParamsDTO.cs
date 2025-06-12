using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoListParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? estados_campanias { get; set; }
        public List<CodeTextDTO>? productos { get; set; }
        public List<CodeTextDTO>? estados { get; set; }
    }
}
