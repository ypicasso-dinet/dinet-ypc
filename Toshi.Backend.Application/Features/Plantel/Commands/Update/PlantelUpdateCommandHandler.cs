using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Update
{
    public class PlantelUpdateCommandHandler : MediatR.IRequestHandler<PlantelUpdateCommand, string>
    {
        protected readonly IPlantelRepository repository;

        public PlantelUpdateCommandHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(PlantelUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
