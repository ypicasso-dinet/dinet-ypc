using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalUpdate
{
    public class PersonalUpdateCommandHandler : MediatR.IRequestHandler<PersonalUpdateCommand, ProveedorPersonalResponseDTO>
    {
        protected readonly IProveedorRepository repository;

        public PersonalUpdateCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorPersonalResponseDTO> Handle(PersonalUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.PersonalUpdate(request);
        }
    }
}
