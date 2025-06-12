using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById
{
    public class IngresoProductoGetByIdQuery : AppBaseCommand, IRequest<IngresoProductoDTO?>
    {
        public string? id { get; set; }
    }
}
