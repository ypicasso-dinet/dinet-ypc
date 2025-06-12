using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetById
{
    public class ProductoGetByIdQuery : AppBaseCommand, IRequest<ProductoDTO?>
    {
        public string? id { get; set; }
    }
}
