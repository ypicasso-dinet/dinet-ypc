using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Producto;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetById
{
    public class ProductoGetByIdQueryHandler : IRequestHandler<ProductoGetByIdQuery, ProductoDTO?>
    {
        protected readonly IProductoRepository repository;

        public ProductoGetByIdQueryHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductoDTO?> Handle(ProductoGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
