using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Delete
{
    public class ProveedorDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
