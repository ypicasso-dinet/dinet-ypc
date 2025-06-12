namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoItemDTO
    {
        public string? id_salida_producto { get; set; }
        public string? nom_plantel { get; set; }
        public string? cod_campania { get; set; }
        public string? fec_registro { get; set; }
        public string? nom_producto { get; set; }
        public string? tip_producto { get; set; }
        public string? uni_producto { get; set; }
        public decimal can_salida { get; set; }
        public bool? cod_estado { get; set; }
        public string? nom_estado { get; set; }
        public string? cod_producto { get; set; }
        public string? cod_plantel { get; set; }
        public string? id_producto { get; set; }

        //
        public bool allow_update { get; set; }
        public bool allow_delete { get; set; }
        public bool? ind_enviado { get; set; }
        public string? nom_enviado { get; set; }

        public string? color { get; set; }
        public List<string> imagenes { get; set; } = new();
    }
}
