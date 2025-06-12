using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.Opciones
{
    public class OpcionesQueryHandler : IRequestHandler<OpcionesQuery, List<RolMenuDTO>>
    {
        private readonly IRolRepository repository;

        public OpcionesQueryHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<RolMenuDTO>> Handle(OpcionesQuery request, CancellationToken cancellationToken)
        {
            return await repository.Opciones(request);
        }
    }
}
