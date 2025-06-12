using Toshi.Backend.Application.Contracts.Persistence;
using MediatR;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Delete
{
    public class UsuarioDeleteCommandHandlder : IRequestHandler<UsuarioDeleteCommand, string>
    {
        private readonly IUsuarioRepository repository;

        public UsuarioDeleteCommandHandlder(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(UsuarioDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
