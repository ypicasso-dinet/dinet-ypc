using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Producto
{
    public class ProductoScreenParamsDTO
    {
        public List<CodeTextDTO>? tipos { get; set; }
        public List<CodeTextDTO>? estados { get; set; }
        public List<CodeTextDTO>? unidades { get; set; }
        public List<CodeTextDTO>? segmentos { get; set; }
    }
}
