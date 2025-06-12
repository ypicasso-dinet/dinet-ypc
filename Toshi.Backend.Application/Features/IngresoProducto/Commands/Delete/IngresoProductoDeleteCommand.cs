using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete
{
    public class IngresoProductoDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
