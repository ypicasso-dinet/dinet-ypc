using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Application.Features.IngresoPollo.Commands.Create;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Create
{
    public class IngresoProductoCreateCommandHandler : MediatR.IRequestHandler<IngresoProductoCreateCommand, string>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoCreateCommandHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoProductoCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
