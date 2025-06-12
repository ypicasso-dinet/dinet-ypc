using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete
{
    public class SalidaProductoDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
