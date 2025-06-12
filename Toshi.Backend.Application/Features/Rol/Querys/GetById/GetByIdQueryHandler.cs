using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, RolDTO?>
    {
        private readonly IRolRepository repository;

        public GetByIdQueryHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<RolDTO?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetById(request);
        }
    }
}
