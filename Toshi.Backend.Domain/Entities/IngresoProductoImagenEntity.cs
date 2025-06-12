namespace Toshi.Backend.Domain.Entities
{
    public partial class IngresoProductoImagenEntity : BaseDomainModel
    {
        public int id_ingreso_producto_imagen { get; set; }
        public string gid_ingreso_producto_imagen { get; set; } = string.Empty;
        public int id_ingreso_producto { get; set; }
        public string nom_imagen { get; set; } = string.Empty;
        public string url_imagen { get; set; } = string.Empty;
        public IngresoProductoEntity? ingreso_producto { get; set; }
    }
}
