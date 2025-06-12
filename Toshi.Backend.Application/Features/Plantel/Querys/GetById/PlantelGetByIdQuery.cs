using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.GetById
{
    public class PlantelGetByIdQuery : AppBaseCommand, IRequest<PlantelDTO?>
    {
        public string? id { get; set; }
    }
}
