using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Update
{
    public class IngresoProductoUpdateCommandHandler : MediatR.IRequestHandler<IngresoProductoUpdateCommand, string>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoUpdateCommandHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(IngresoProductoUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
