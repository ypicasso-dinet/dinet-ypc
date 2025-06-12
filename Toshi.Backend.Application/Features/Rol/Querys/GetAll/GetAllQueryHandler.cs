using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<RolItemDTO>>
    {
        protected readonly IRolRepository repository;

        public GetAllQueryHandler(IRolRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<RolItemDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
