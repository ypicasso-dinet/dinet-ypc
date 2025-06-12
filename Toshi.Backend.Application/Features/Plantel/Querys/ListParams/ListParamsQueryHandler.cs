using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.ListParams
{
    public class ListParamsQueryHandler : MediatR.IRequestHandler<ListParamsQuery, PlantelListParamsDTO>
    {
        protected readonly IPlantelRepository repository;

        public ListParamsQueryHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PlantelListParamsDTO> Handle(ListParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ListParams(request);
        }
    }
}
