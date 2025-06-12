using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalDelete
{
    public class PersonalDeleteCommand : AppBaseCommand, MediatR.IRequest<ProveedorPersonalResponseDTO>
    {
        // Request Properties
        public string? id_proveedor { get; set; }
        //public string? id_rol { get; set; }
        public string? id_personal { get; set; }
    }
}
