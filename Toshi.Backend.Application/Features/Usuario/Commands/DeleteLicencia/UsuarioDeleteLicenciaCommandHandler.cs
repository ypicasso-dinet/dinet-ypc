using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia
{
    public class UsuarioDeleteLicenciaCommandHandler : MediatR.IRequestHandler<UsuarioDeleteLicenciaCommand, string>
    {
        protected readonly IUsuarioRepository repository;

        public UsuarioDeleteLicenciaCommandHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(UsuarioDeleteLicenciaCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteLicencia(request);
        }
    }
}
