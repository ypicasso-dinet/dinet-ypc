using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalCreate
{
    public class PersonalCreateCommandHandler : MediatR.IRequestHandler<PersonalCreateCommand, ProveedorPersonalResponseDTO>
    {
        protected readonly IProveedorRepository repository;

        public PersonalCreateCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorPersonalResponseDTO> Handle(PersonalCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.PersonalCreate(request);
        }
    }
}
