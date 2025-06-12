using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetUnidadMedidaPorProductoHandler : IRequestHandler<IngresoProductoGetUnidadMedidaPorProductoQuery, IngresoProductoGetUnidadMedidaPorProductoDTO>
    {
        protected readonly IIngresoProductoRepository _repo;

        public IngresoProductoGetUnidadMedidaPorProductoHandler(IIngresoProductoRepository repo)
        {
            _repo = repo;
        }

        public async  Task<IngresoProductoGetUnidadMedidaPorProductoDTO> Handle(IngresoProductoGetUnidadMedidaPorProductoQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetUnidadMedidaPorProducto(request);
        }
    }
}
