using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Commands.UserAppend
{
    public class UserAppendCommand : AppBaseCommand, MediatR.IRequest<PlantelUserResponseDTO>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public string? id_rol { get; set; }
        public List<PlantelUsuarioDTO>? usuarios { get; set; }
    }
}
