using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.BuscarPersona
{
    public class BuscarPersonaCommandHandler : MediatR.IRequestHandler<BuscarPersonaCommand, ProveedorPersonaDTO?>
    {
        protected readonly IProveedorRepository repository;

        public BuscarPersonaCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorPersonaDTO?> Handle(BuscarPersonaCommand request, CancellationToken cancellationToken)
        {
            return await repository.BuscarPersona(request);
        }
    }
}
