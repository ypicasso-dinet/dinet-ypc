using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetTipoProductoPorProductoHandler : IRequestHandler<IngresoProductoGetTipoProductoPorProductoQuery, IngresoProductoGetTipoProductoPorProductoDTO>
    {
        protected readonly IIngresoProductoRepository _repo;

        public IngresoProductoGetTipoProductoPorProductoHandler(IIngresoProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IngresoProductoGetTipoProductoPorProductoDTO> Handle(IngresoProductoGetTipoProductoPorProductoQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetTipoProductoPorProducto(request);
        }
    }
}
