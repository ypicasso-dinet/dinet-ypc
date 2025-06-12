using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetAll
{
    public class IngresoProductoGetAllQuery : AppBaseCommand, IRequest<IngresoProductoListResponseDTO>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? cod_estado_campania { get; set; }
        public string? id_producto { get; set; }
        public string? cod_estado { get; set; }
        public string? fec_desde { get; set; }
        public string? fec_hasta { get; set; }
    }
}
