namespace Toshi.Backend.Domain.Entities
{
    public partial class SalidaProductoImagenEntity : BaseDomainModel
    {
        public int id_salida_producto_imagen { get; set; }
        public string gid_salida_producto_imagen { get; set; } = string.Empty;
        public int id_salida_producto { get; set; }
        public string nom_imagen { get; set; } = string.Empty;
        public string url_imagen { get; set; } = string.Empty;
        public SalidaProductoEntity? salida_producto { get; set; }
    }
}
