using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoScreenParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? productos { get; set; }
        public List<CodeTextDTO>? tipo_producto { get; set; }
        public List<CodeTextDTO>? und_medida { get; set; }
    }
}
