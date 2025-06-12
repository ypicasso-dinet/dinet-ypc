using MediatR;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Update
{
    public class RolUpdateCommand : AppBaseCommand, IRequest<string>
    {
        public string? id { get; set; }
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
    }
}
