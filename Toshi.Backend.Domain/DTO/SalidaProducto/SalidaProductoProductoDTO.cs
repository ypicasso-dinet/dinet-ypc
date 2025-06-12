namespace Toshi.Backend.Domain.DTO.SalidaProducto
{
    public class SalidaProductoProductoDTO
    {
        public string? id_producto { get; set; }
        public string? nom_producto { get; set; }
        public string? tip_producto { get; set; }
        public string? uni_producto { get; set; }
        public string? code { get { return this.id_producto; } }
        public string? text { get; set; }
    }
}
