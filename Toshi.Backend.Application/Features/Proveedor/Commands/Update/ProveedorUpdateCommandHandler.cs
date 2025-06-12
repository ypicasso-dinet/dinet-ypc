using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Update
{
    public class ProveedorUpdateCommandHandler : MediatR.IRequestHandler<ProveedorUpdateCommand, string>
    {
        protected readonly IProveedorRepository repository;

        public ProveedorUpdateCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ProveedorUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
