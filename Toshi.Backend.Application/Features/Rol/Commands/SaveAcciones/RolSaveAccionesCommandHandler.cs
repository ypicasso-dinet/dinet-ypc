using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones
{
    public class RolSaveAccionesCommandHandler : IRequestHandler<RolSaveAccionesCommand, string>
    {
        private readonly IRolRepository repository;

        public RolSaveAccionesCommandHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(RolSaveAccionesCommand request, CancellationToken cancellationToken)
        {
            return await repository.SaveAcciones(request);
        }
    }
}
