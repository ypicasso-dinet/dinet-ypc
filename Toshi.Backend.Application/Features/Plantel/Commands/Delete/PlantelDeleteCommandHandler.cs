using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Delete
{
    public class PlantelDeleteCommandHandler : MediatR.IRequestHandler<PlantelDeleteCommand, string>
    {
        protected readonly IPlantelRepository repository;

        public PlantelDeleteCommandHandler(IPlantelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(PlantelDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
