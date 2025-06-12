using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetCampaniasPorEstadoHandler : IRequestHandler<IngresoProductoGetCampaniasPorEstadoQuery, IngresoProductoListCampaniaPorEstadoDTO>
    {
        protected readonly IIngresoProductoRepository _repo;

        public IngresoProductoGetCampaniasPorEstadoHandler(IIngresoProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IngresoProductoListCampaniaPorEstadoDTO> Handle(IngresoProductoGetCampaniasPorEstadoQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetCampaniasByEstadoAsync(request);
        }
    }
}
