using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Rol.Commands.Delete
{
    public class RolDeleteCommandHandler : IRequestHandler<RolDeleteCommand, string>
    {
        private readonly IRolRepository repository;

        public RolDeleteCommandHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(RolDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
