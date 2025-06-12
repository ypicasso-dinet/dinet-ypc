using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.SalidaProducto;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll
{
    public class SalidaProductoGetAllQueryHandler : IRequestHandler<SalidaProductoGetAllQuery, SalidaProductoGetAllResponseDTO>
    {
        protected readonly ISalidaProductoRepository repository;

        public SalidaProductoGetAllQueryHandler(ISalidaProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SalidaProductoGetAllResponseDTO> Handle(SalidaProductoGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
