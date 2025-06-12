using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.IngresoPollo
{
    public class IngresoPolloScreenParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? galpones { get; set; }
        public List<CodeTextDTO>? sexos { get; set; }
        public List<CodeTextDTO>? edades { get; set; }
        public List<CodeTextDTO>? lotes { get; set; }
        public List<CodeTextDTO>? lineas { get; set; }
    }
}
