using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalDelete
{
    public class PersonalDeleteCommandHandler : MediatR.IRequestHandler<PersonalDeleteCommand, ProveedorPersonalResponseDTO>
    {
        protected readonly IProveedorRepository repository;

        public PersonalDeleteCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorPersonalResponseDTO> Handle(PersonalDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.PersonalDelete(request);
        }
    }
}
