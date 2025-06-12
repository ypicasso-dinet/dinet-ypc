using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete
{
    public class IngresoProductoDeleteCommandHandler : MediatR.IRequestHandler<IngresoProductoDeleteCommand, string>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoDeleteCommandHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoProductoDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
