using Toshi.Backend.Application.Contracts.Persistence;

namespace Toshi.Backend.Application.Features.Producto.Commands.Update
{
    public class ProductoUpdateCommandHandler : MediatR.IRequestHandler<ProductoUpdateCommand, string>
    {
        protected readonly IProductoRepository repository;

        public ProductoUpdateCommandHandler(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(ProductoUpdateCommand request, CancellationToken cancellationToken)
        {
            return await repository.Update(request);
        }
    }
}
