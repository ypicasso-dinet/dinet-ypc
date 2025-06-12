using MediatR;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Delete
{
    public class RolDeleteCommand : AppBaseCommand, IRequest<string>
    {
        public string? id { get; set; }

        public RolDeleteCommand(string? id)
        {
            this.id = id;
        }
    }
}
