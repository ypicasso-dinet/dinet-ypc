using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Estandar
{
    public class EstandarScreenParamsDTO
    {
        public List<CodeTextDTO>? lotes { get; set; }
        public List<CodeTextDTO>? edades { get; set; }
        public List<CodeTextDTO>? sexos { get; set; }
    }
}
