using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Delete
{
    public class ProveedorDeleteCommandHandler : MediatR.IRequestHandler<ProveedorDeleteCommand, string>
    {
        protected readonly IProveedorRepository repository;

        public ProveedorDeleteCommandHandler(IProveedorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ProveedorDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
