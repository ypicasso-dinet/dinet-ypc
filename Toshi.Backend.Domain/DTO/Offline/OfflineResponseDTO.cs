using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Offline
{
    public class OfflineResponseDTO
    {
        public List<OfflinePlantelDTO>? planteles { get; set; }
        public List<CodeTextDTO>? sexos { get; set; }
        public List<CodeTextDTO>? edades { get; set; }
        public List<CodeTextDTO>? lotes { get; set; }
        public List<CodeTextDTO>? lineas { get; set; }

        public List<CodeTextDTO>? estados_ipbb { get; set; }

        public List<OfflineProductoDTO>? productos { get; set; }
    }
}
