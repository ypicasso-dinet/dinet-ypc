using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Producto.Commands.Delete
{
    public class ProductoDeleteCommandHandler : MediatR.IRequestHandler<ProductoDeleteCommand, string>
    {
        protected readonly IProductoRepository repository;

        public ProductoDeleteCommandHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ProductoDeleteCommand request, CancellationToken cancellationToken)
        {
            return await repository.Delete(request);
        }
    }
}
