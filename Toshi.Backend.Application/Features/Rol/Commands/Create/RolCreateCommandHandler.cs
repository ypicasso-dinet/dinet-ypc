
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Rol.Commands.Create
{
    public class RolCreateCommandHandler : MediatR.IRequestHandler<RolCreateCommand, string>
    {
        protected readonly IRolRepository repository;

        public RolCreateCommandHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(RolCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
