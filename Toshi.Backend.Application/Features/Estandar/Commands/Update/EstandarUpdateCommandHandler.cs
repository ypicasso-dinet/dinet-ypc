using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Update
{
    public class EstandarUpdateCommandHandler : MediatR.IRequestHandler<EstandarUpdateCommand, string>
    {
        protected readonly IEstandarRepository repository;

        public EstandarUpdateCommandHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(EstandarUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
