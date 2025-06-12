using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Commands.Create
{
    public class ProductoCreateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? cod_producto { get; set; }
        public string? nom_producto { get; set; }
        public string? uni_medida { get; set; }
        public string? cod_tipo { get; set; }
        public string? cod_segmento { get; set; }
        public decimal? min_ingreso { get; set; }
        public decimal? max_ingreso { get; set; }
        public decimal? min_salida { get; set; }
        public decimal? max_salida { get; set; }
        public decimal? min_transfer { get; set; }
        public decimal? max_transfer { get; set; }
        public string? nom_estado { get; set; }
    }
}
