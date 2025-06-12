using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.GetAll
{
    public class ProveedorGetAllQuery : AppBaseCommand, IRequest<List<ProveedorItemDTO>>
    {
        // Request Properties
        public string? ruc_proveedor { get; set; }
        public string? nom_proveedor { get; set; }
    }
}
