using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Create
{
    public class SalidaProductoCreateCommandHandler : MediatR.IRequestHandler<SalidaProductoCreateCommand, string>
    {
        protected readonly ISalidaProductoRepository repository;

        public SalidaProductoCreateCommandHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(SalidaProductoCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
