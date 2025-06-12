using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Campania
{
    public class CampaniaScreenParamsDTO
    {
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? anios { get; set; }
        public List<CodeTextDTO>? estados { get; set; }
    }
}
