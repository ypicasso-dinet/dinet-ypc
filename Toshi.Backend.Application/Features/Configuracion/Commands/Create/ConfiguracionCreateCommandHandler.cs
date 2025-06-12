using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Create
{
    public class ConfiguracionCreateCommandHandler : MediatR.IRequestHandler<ConfiguracionCreateCommand, string>
    {
        protected readonly IConfiguracionRepository repository;

        public ConfiguracionCreateCommandHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ConfiguracionCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
