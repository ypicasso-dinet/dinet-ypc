using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.BuscarPersona
{
    public class BuscarPersonaCommand : AppBaseCommand, MediatR.IRequest<ProveedorPersonaDTO?>
    {
        // Request Properties
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
    }
}
