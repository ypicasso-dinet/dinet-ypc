using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania
{
    public class CerrarCampaniaCommandHandler : MediatR.IRequestHandler<CerrarCampaniaCommand, string>
    {
        protected readonly ICampaniaRepository repository;

        public CerrarCampaniaCommandHandler(ICampaniaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(CerrarCampaniaCommand request, CancellationToken cancellationToken)
        {
            return await repository.CerrarCampania(request);
        }
    }
}
