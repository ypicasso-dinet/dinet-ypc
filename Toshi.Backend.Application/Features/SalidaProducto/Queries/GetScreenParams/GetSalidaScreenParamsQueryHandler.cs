using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetScreenParams
{
    public class GetSalidaScreenParamsQueryHandler : MediatR.IRequestHandler<GetSalidaScreenParamsQuery, SalidaProductoScreenParamsDTO>
    {
        protected readonly ISalidaProductoRepository repository;

        public GetSalidaScreenParamsQueryHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SalidaProductoScreenParamsDTO> Handle(GetSalidaScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetScreenParams(request);
        }
    }
}
