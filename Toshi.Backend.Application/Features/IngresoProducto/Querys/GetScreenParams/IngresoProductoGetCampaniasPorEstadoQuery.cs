using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Toshi.Backend.Domain.DTO.Campania;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetCampaniasPorEstadoQuery : IRequest<IngresoProductoListCampaniaPorEstadoDTO>
    {
        public string CodEstadoCampania { get; set; } = string.Empty;
        public string Id_plantel { get; set; }
    }
}
