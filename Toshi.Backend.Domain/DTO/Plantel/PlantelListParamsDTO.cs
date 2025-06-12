using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Plantel
{
    public class PlantelListParamsDTO
    {
        public List<CodeTextDTO>? estados { get; set; }
        public List<PlantelRolDTO>? roles { get; set; }
    }
}
