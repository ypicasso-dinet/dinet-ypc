using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoTab2DTO
    {
        public string? cod_producto { get; set; }
        public string? nom_producto { get; set; }
        public string? uni_medida { get; set; }
        public decimal? can_ingreso { get; set; }
        public decimal? can_salida { get; set; }
        public decimal? can_saldo { get { return (can_ingreso ?? 0) - (can_salida ?? 0); } }
    }
}
