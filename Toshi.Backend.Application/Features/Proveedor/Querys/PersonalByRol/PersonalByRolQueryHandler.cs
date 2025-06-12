using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.UsersByRol
{
    public class PersonalByRolQueryHandler : MediatR.IRequestHandler<PersonalByRolQuery, List<ProveedorRolPersonalDTO>>
    {
        protected readonly IProveedorRepository repository;

        public PersonalByRolQueryHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProveedorRolPersonalDTO>> Handle(PersonalByRolQuery request, CancellationToken cancellationToken)
        {
            return await repository.PersonalByRol(request);
        }
    }
}
