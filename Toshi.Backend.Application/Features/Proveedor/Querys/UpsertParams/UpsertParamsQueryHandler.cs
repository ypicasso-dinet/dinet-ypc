using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.UpsertParams
{
    public class UpsertParamsQueryHandler : MediatR.IRequestHandler<UpsertParamsQuery, ProveedorUpsertParamsDTO>
    {
        protected readonly IProveedorRepository repository;

        public UpsertParamsQueryHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorUpsertParamsDTO> Handle(UpsertParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.UpsertParams(request);
        }
    }
}
