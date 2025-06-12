using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetListParams
{
    public class IngresoProductoGetListParamsQueryHandler : MediatR.IRequestHandler<IngresoProductoGetListParamsQuery, IngresoProductoListParamsDTO>
    {
        protected readonly IIngresoProductoRepository repository;

        public IngresoProductoGetListParamsQueryHandler(IIngresoProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IngresoProductoListParamsDTO> Handle(IngresoProductoGetListParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetListParams(request);
        }
    }
}
