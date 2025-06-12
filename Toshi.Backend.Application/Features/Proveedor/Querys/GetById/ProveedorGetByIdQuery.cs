using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.GetById
{
    public class ProveedorGetByIdQuery : AppBaseCommand, IRequest<ProveedorDTO?>
    {
        public string? id { get; set; }
    }
}
