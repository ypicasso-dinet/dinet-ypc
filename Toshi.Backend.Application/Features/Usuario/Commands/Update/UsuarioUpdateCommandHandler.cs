using Toshi.Backend.Application.Contracts.Persistence;
using MediatR;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Update
{
    public class UsuarioUpdateCommandHandler : IRequestHandler<UsuarioUpdateCommand, string>
    {
        private readonly IUsuarioRepository repository;

        public UsuarioUpdateCommandHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(UsuarioUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
