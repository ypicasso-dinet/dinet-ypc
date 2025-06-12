using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll
{
    public class SalidaProductoGetAllQuery : AppBaseCommand, IRequest<SalidaProductoGetAllResponseDTO>
    {
        // Request Properties
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? nom_producto { get; set; }
        public string? cod_estado { get; set; }
        public string? fec_desde { get; set; }
        public string? fec_hasta { get; set; }
        public string? id_producto { get; set; }
    }
}
