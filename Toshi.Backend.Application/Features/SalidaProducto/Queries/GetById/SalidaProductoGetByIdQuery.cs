using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById
{
    public class SalidaProductoGetByIdQuery : AppBaseCommand, IRequest<SalidaProductoDTO?>
    {
        public string? id { get; set; }
    }
}
