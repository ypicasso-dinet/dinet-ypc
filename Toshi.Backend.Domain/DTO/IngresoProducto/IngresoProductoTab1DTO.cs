using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toshi.Backend.Domain.DTO.IngresoProducto
{
    public class IngresoProductoTab1DTO
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
        public string? cod_producto { get; set; }
        public string? desc_producto { get; set; }
        public string? cod_plantel { get; set; }

        public string? nom_plantel { get; set; }
        public string? nom_producto { get; set; }
        public string? nom_campania { get; set; }
        public string? unidad_medida { get; set; }
        public string? tipo_producto { get; set; }

        public string? nom_estado { get; set; }
        public bool? cod_estado { get; set; }

        public bool allow_update { get; set; }
        public bool allow_delete { get; set; }
        public bool? ind_enviado { get; set; }
        public string? nom_enviado { get; set; }

        public string? color { get; set; }
        public List<string> imagenes { get; set; } = new();
    }
}
