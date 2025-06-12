using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toshi.Backend.Domain.DTO.Common;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoGetTipoProductoPorProductoDTO
    {
        public List<CodeTextDTO>? tipo_producto { get; set; }
    }
}
