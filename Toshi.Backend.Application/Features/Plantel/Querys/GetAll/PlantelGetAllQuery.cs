using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.GetAll
{
    public class PlantelGetAllQuery : AppBaseCommand, IRequest<List<PlantelItemDTO>>
    {
        // Request Properties
        public string? cod_plantel { get; set; }
        public string? nom_plantel { get; set; }
        public string? cod_estado { get; set; }
    }
}
