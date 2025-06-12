using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams
{
    public class UpsertParamsQuery : MediatR.IRequest<PlantelUpsertParamsDTO>
    {
        // Request Properties
        public string? id_plantel { get; set; }
    }
}
