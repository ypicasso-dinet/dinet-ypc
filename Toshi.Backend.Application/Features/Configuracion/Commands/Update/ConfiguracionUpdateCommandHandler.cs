using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Update
{
    public class ConfiguracionUpdateCommandHandler : MediatR.IRequestHandler<ConfiguracionUpdateCommand, string>
    {
        protected readonly IConfiguracionRepository repository;

        public ConfiguracionUpdateCommandHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ConfiguracionUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
