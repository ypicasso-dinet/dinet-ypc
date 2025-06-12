using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.GetById
{
    public class ProveedorGetByIdQueryHandler : IRequestHandler<ProveedorGetByIdQuery, ProveedorDTO?>
    {
        protected readonly IProveedorRepository repository;

        public ProveedorGetByIdQueryHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorDTO?> Handle(ProveedorGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
