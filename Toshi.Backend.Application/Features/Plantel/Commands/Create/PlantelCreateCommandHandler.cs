using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Create
{
    public class PlantelCreateCommandHandler : MediatR.IRequestHandler<PlantelCreateCommand, StatusResponse>
    {
        protected readonly IPlantelRepository repository;

        public PlantelCreateCommandHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<StatusResponse> Handle(PlantelCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
