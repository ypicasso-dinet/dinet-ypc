using System.Text.Json.Serialization;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Create
{
    public class SalidaProductoCreateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_campania { get; set; }
        public string? fec_registro { get; set; }
        public string? id_producto { get; set; }
        public decimal? can_salida { get; set; }
        public string? observacion { get; set; }
        public string? id_plantel { get; set; }

        [JsonIgnore]
        public bool is_mobile { get; set; }

        public List<SalidaProductoImagenDTO>? imagenes { get; set; }
    }
}
