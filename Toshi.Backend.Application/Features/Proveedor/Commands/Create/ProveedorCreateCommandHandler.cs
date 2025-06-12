using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Create
{
    public class ProveedorCreateCommandHandler : MediatR.IRequestHandler<ProveedorCreateCommand, ProveedorCreateResponseDTO>
    {
        protected readonly IProveedorRepository repository;

        public ProveedorCreateCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProveedorCreateResponseDTO> Handle(ProveedorCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
