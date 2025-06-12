using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Commands.Delete
{
    public class ProductoDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
