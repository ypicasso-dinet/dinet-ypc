using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById
{
    public class SalidaProductoGetByIdQueryHandler : IRequestHandler<SalidaProductoGetByIdQuery, SalidaProductoDTO?>
    {
        protected readonly ISalidaProductoRepository repository;

        public SalidaProductoGetByIdQueryHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SalidaProductoDTO?> Handle(SalidaProductoGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
