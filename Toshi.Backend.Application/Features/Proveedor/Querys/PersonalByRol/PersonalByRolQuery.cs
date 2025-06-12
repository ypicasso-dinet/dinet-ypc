using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.UsersByRol
{
    public class PersonalByRolQuery : MediatR.IRequest<List<ProveedorRolPersonalDTO>>
    {
        // Request Properties
        public string? id_proveedor { get; set; }
        public string? id_rol { get; set; }
    }
}
