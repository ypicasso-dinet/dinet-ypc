using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetListParams
{
    public class GetSalidaListParamsQueryHandler : MediatR.IRequestHandler<GetSalidaListParamsQuery, SalidaProductoListParamsDTO>
    {
        protected readonly ISalidaProductoRepository repository;

        public GetSalidaListParamsQueryHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SalidaProductoListParamsDTO> Handle(GetSalidaListParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetListParams(request);
        }
    }
}
