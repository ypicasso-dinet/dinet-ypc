using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Update
{
    public class ProveedorUpdateCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id_proveedor { get; set; }
        public string? ruc_proveedor { get; set; }
        public string? nom_proveedor { get; set; }
    }
}
