using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetAll
{
    public class ProductoGetAllQuery : AppBaseCommand, IRequest<List<ProductoItemDTO>>
    {
        // Request Properties
        public string? cod_producto { get; set; }
        public string? nom_producto { get; set; }
        public string? tip_producto { get; set; }
        public string? cod_estado { get; set; }
    }
}
