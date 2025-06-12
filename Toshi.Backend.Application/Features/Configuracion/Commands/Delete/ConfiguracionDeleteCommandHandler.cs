using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Delete
{
    public class ConfiguracionDeleteCommandHandler : MediatR.IRequestHandler<ConfiguracionDeleteCommand, string>
    {
        protected readonly IConfiguracionRepository repository;

        public ConfiguracionDeleteCommandHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ConfiguracionDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
