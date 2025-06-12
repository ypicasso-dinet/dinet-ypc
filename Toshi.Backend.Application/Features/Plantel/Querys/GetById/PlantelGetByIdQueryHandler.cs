using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Plantel;

namespace Toshi.Backend.Application.Features.Plantel.Querys.GetById
{
    public class PlantelGetByIdQueryHandler : IRequestHandler<PlantelGetByIdQuery, PlantelDTO?>
    {
        protected readonly IPlantelRepository repository;

        public PlantelGetByIdQueryHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PlantelDTO?> Handle(PlantelGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
