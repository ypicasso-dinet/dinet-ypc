using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetAll
{
    public class ProductoGetAllQueryHandler : IRequestHandler<ProductoGetAllQuery, List<ProductoItemDTO>>
    {
        protected readonly IProductoRepository repository;

        public ProductoGetAllQueryHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProductoItemDTO>> Handle(ProductoGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
