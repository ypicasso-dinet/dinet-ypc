namespace Toshi.Backend.Domain.Entities
{
    public partial class ProductoEntity : BaseDomainModel
    {
        public int id_producto { get; set; }
        public string gid_producto { get; set; } = string.Empty;
        public string cod_producto { get; set; } = string.Empty;
        public string nom_producto { get; set; } = string.Empty;
        public string uni_medida { get; set; } = string.Empty;
        public string cod_tipo { get; set; } = string.Empty;
        public string cod_segmento { get; set; } = string.Empty;
        public decimal? min_ingreso { get; set; }
        public decimal? max_ingreso { get; set; }
        public decimal? min_salida { get; set; }
        public decimal? max_salida { get; set; }
        public decimal? min_transfer { get; set; }
        public decimal? max_transfer { get; set; }
        public List<IngresoProductoEntity>? ingreso_productos { get; set; }
        public List<SalidaProductoEntity>? salida_productos { get; set; }
    }
}
