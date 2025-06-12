using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Update
{
    public class PlantelUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public string? cod_plantel { get; set; }
        public string? nom_plantel { get; set; }
        public bool? ind_interno { get; set; }
    }
}
