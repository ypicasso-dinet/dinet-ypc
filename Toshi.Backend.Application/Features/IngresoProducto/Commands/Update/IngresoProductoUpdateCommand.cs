using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Application.Features.IngresoProducto.Commands.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Update
{
    public class IngresoProductoUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_ingreso_producto { get; set; }
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? id_producto { get; set; }
        public string? fec_registro { get; set; }
        public decimal? can_ingreso { get; set; }
        public string? guia_recepcion { get; set; }
        public string? guia_proveedor { get; set; }
        public string? observacion { get; set; }
        public string? unidad_medida { get; set; }
        public string? tipo_producto { get; set; }

        public List<IngresoProductoImagen>? imagenes { get; set; }
    }
}
