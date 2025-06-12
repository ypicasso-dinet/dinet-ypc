using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Campania.Commands.CreateCampania
{
    public class CreateCampaniaCommandHandler : MediatR.IRequestHandler<CreateCampaniaCommand, string>
    {
        protected readonly ICampaniaRepository repository;

        public CreateCampaniaCommandHandler(ICampaniaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(CreateCampaniaCommand request, CancellationToken cancellationToken)
        {
            return await repository.CreateCampania(request);
        }
    }
}
