using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete
{
    public class SalidaProductoDeleteCommandHandler : MediatR.IRequestHandler<SalidaProductoDeleteCommand, string>
    {
        protected readonly ISalidaProductoRepository repository;

        public SalidaProductoDeleteCommandHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(SalidaProductoDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
