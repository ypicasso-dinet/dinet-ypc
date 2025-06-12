using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Domain.DTO.Usuario
{
    public class UsuarioScreenParamsDTO
    {
        public List<CodeTextDTO>? estados { get; set; }

        public List<CodeTextDTO>? tipos_documento { get; set; }
        public List<CodeTextDTO>? tipos_usuario { get; set; }
        public List<RolItemDTO>? roles { get; set; }
        public List<UsuarioPlantelDTO>? planteles { get; set; }
        public List<CodeTextDTO>? proveedores { get; set; }

    }
}
