using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Delete
{
    public class EstandarDeleteCommandHandler : MediatR.IRequestHandler<EstandarDeleteCommand, string>
    {
        protected readonly IEstandarRepository repository;

        public EstandarDeleteCommandHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(EstandarDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
