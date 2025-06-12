using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetAll
{
    public class IngresoPolloGetAllQuery : AppBaseCommand, IRequest<IngresoPolloListResponseDTO>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? num_galpon { get; set; }
        public string? cod_estado { get; set; }
        public string? fec_desde { get; set; }
        public string? fec_hasta { get; set; }
    }
}
