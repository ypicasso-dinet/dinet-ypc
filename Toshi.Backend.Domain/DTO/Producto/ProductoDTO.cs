namespace Toshi.Backend.Domain.DTO.Producto
{
    public class ProductoDTO
    {
        public string? id_producto { get; set; }
        public string? cod_producto { get; set; }
        public string? nom_producto { get; set; }
        public string? uni_medida { get; set; }
        public string? nom_tipo { get; set; }
        public string? nom_segmento { get; set; }
        public decimal? min_ingreso { get; set; }
        public decimal? max_ingreso { get; set; }
        public decimal? min_salida { get; set; }
        public decimal? max_salida { get; set; }
        public decimal? min_transfer { get; set; }
        public decimal? max_transfer { get; set; }
        public string? nom_estado { get; set; }
    }
}
