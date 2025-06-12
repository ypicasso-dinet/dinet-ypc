using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetScreenParamsQueryHandler : MediatR.IRequestHandler<IngresoProductoGetScreenParamsQuery, IngresoProductoScreenParamsDTO>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoGetScreenParamsQueryHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoProductoScreenParamsDTO> Handle(IngresoProductoGetScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetScreenParams(request);
        }
    }
}
