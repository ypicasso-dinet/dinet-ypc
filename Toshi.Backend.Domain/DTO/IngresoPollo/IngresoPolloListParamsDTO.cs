using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.IngresoPollo
{
    public class IngresoPolloListParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? galpones { get; set; }
        public List<CodeTextDTO>? estados { get; set; }
    }
}
