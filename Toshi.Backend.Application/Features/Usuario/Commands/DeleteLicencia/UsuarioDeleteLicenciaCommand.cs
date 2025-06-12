using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia
{
    public class UsuarioDeleteLicenciaCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_licencia { get; set; }
        public string? id_usuario { get; set; }
    }
}
