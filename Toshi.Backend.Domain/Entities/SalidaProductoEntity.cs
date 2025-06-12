namespace Toshi.Backend.Domain.Entities
{
    public partial class SalidaProductoEntity : BaseDomainModel
    {
        public int id_salida_producto { get; set; }
        public string gid_salida_producto { get; set; } = string.Empty;
        public int id_usuario { get; set; }
        public int id_campania { get; set; }
        public DateTime fec_registro { get; set; }
        public int id_producto { get; set; }
        public decimal can_salida { get; set; }
        public string? tip_producto { get; set; }
        public string? uni_producto { get; set; }
        public string? observacion { get; set; }
        public CampaniaEntity? campania { get; set; }
        public ProductoEntity? producto { get; set; }
        public UsuarioEntity? usuario { get; set; }
        public List<SalidaProductoImagenEntity>? salida_producto_imagenes { get; set; }
    }
}
