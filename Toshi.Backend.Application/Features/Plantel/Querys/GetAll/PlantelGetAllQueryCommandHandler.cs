using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.GetAll
{
    public class PlantelGetAllQueryHandler : IRequestHandler<PlantelGetAllQuery, List<PlantelItemDTO>>
    {
        protected readonly IPlantelRepository repository;

        public PlantelGetAllQueryHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<PlantelItemDTO>> Handle(PlantelGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
