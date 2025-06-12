using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Producto.Commands.Create
{
    public class ProductoCreateCommandHandler : MediatR.IRequestHandler<ProductoCreateCommand, string>
    {
        protected readonly IProductoRepository repository;

        public ProductoCreateCommandHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ProductoCreateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Create(request);
        }
    }
}
