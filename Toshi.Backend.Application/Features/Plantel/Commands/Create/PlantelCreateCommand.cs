using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Create
{
    public class PlantelCreateCommand : MediatR.IRequest<StatusResponse>
    {
        // Request Properties
        public string? cod_plantel { get; set; }
        public string? nom_plantel { get; set; }
    }
}
