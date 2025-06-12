using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.Application.Features.Producto.Querys.ScreenParams
{
    public class ScreenParamsQueryHandler : MediatR.IRequestHandler<ScreenParamsQuery, ProductoScreenParamsDTO>
    {
        protected readonly IProductoRepository repository;

        public ScreenParamsQueryHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductoScreenParamsDTO> Handle(ScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParams(request);
        }
    }
}
