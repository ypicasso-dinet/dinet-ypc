using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia
{
    public class UpsertLicenciaCommandHandler : MediatR.IRequestHandler<UpsertLicenciaCommand, UsuarioLicenciaResponseDTO>
    {
        protected readonly IUsuarioRepository repository;

        public UpsertLicenciaCommandHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UsuarioLicenciaResponseDTO> Handle(UpsertLicenciaCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpsertLicencia(request);
        }
    }
}
