using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.GetAll
{
    public class ProveedorGetAllQueryHandler : IRequestHandler<ProveedorGetAllQuery, List<ProveedorItemDTO>>
    {
        protected readonly IProveedorRepository repository;

        public ProveedorGetAllQueryHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProveedorItemDTO>> Handle(ProveedorGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
