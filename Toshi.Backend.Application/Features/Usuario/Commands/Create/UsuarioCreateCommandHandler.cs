using Toshi.Backend.Application.Contracts.Persistence;
using MediatR;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Create
{
    public class UsuarioCreateCommandHandler : IRequestHandler<UsuarioCreateCommand, UsuarioCreateResponseDTO>
    {
        private readonly IUsuarioRepository repository;

        public UsuarioCreateCommandHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UsuarioCreateResponseDTO> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
