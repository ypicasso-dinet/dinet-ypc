using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Update
{
    public class SalidaProductoUpdateCommandHandler : MediatR.IRequestHandler<SalidaProductoUpdateCommand, string>
    {
        protected readonly ISalidaProductoRepository repository;

        public SalidaProductoUpdateCommandHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(SalidaProductoUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
