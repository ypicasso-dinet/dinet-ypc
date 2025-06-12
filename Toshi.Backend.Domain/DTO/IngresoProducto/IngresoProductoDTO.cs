using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toshi.Backend.Domain.DTO.IngresoPollo;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoDTO
    {
        public string? id_ingreso_producto { get; set; }
        public string? id_plantel { get; set; }
        public string? id_campania { get; set; }
        public string? fec_registro { get; set; }
        public string? id_producto { get; set; }
        public decimal? can_ingreso { get; set; }
        public string? guia_recepcion { get; set; }
        public string? guia_proveedor { get; set; }
        public string? observacion { get; set; }
        public string? fec_upsert { get; set; }

        public string? nom_plantel { get; set; }
        public string? nom_producto { get; set; }

        public bool allow_update { get; set; }

        public List<IngresoProductoImagenDTO>? imagenes { get; set; }
    }
}
