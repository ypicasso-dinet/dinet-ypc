using System.Text.Json.Serialization;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Update
{
    public class SalidaProductoUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_salida_producto { get; set; }
        public string? id_campania { get; set; }
        public string? fec_registro { get; set; }
        public string? id_producto { get; set; }
        public decimal? can_salida { get; set; }
        public string? observacion { get; set; }

        [JsonIgnore]
        public bool is_mobile { get; set; }

        public List<SalidaProductoImagenDTO>? imagenes { get; set; }
    }
}
