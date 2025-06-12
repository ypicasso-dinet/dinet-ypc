using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Toshi.Backend.Domain.DTO.IngresoProducto;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetTipoProductoPorProductoQuery : IRequest<IngresoProductoGetTipoProductoPorProductoDTO>
    {
        public string CodProducto { get; set; } = string.Empty;
    }
}
