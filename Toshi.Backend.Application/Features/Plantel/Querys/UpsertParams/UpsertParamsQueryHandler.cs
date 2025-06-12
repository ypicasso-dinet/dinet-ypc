using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams
{
    public class UpsertParamsQueryHandler : MediatR.IRequestHandler<UpsertParamsQuery, PlantelUpsertParamsDTO>
    {
        protected readonly IPlantelRepository repository;

        public UpsertParamsQueryHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PlantelUpsertParamsDTO> Handle(UpsertParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.UpsertParams(request);
        }
    }
}
