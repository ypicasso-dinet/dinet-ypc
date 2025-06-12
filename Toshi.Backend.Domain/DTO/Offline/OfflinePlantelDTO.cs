using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Offline
{
    public class OfflinePlantelDTO
    {
        public string? id_plantel { get; set; }
        public string? nom_plantel { get; set; }

        public List<OfflinePlantelCampaniaDTO>? campanias { get; set; }
        public List<CodeTextDTO>? galpones { get; set; }
    }
}
