namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoDTO
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
        public string? observacion { get; set; }

        public List<SalidaProductoImagenDTO>? imagenes { get; set; }

    }
}
