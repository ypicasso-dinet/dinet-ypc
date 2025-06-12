using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Create
{
    public class EstandarCreateCommandHandler : MediatR.IRequestHandler<EstandarCreateCommand, string>
    {
        protected readonly IEstandarRepository repository;

        public EstandarCreateCommandHandler(IEstandarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(EstandarCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
