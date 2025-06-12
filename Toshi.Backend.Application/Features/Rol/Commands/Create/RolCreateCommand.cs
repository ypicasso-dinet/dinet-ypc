using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Create
{
    public class RolCreateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
    }
}
