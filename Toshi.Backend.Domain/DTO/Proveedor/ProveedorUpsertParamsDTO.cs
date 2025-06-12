using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.Proveedor
{
    public class ProveedorUpsertParamsDTO
    {
        public List<CodeTextDTO>? tipos_documento { get; set; }
        public List<CodeTextDTO>? planteles { get; set; }
        public List<CodeTextDTO>? roles { get; set; }
    }
}
