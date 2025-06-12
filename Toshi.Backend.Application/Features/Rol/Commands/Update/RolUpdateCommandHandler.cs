using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Rol.Commands.Update
{
    public class RolUpdateCommandHandler : IRequestHandler<RolUpdateCommand, string>
    {
        private readonly IRolRepository repository;

        public RolUpdateCommandHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(RolUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
