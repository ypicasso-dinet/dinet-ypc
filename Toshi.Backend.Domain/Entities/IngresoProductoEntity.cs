namespace Toshi.Backend.Domain.Entities
{
    public partial class IngresoProductoEntity : BaseDomainModel
    {
        public int id_ingreso_producto { get; set; }
        public string gid_ingreso_producto { get; set; } = string.Empty;
        public int id_usuario { get; set; }
        public int id_campania { get; set; }
        public DateTime fec_registro { get; set; }
        public int id_producto { get; set; }
        public decimal can_ingreso { get; set; }
        public string guia_recepcion { get; set; } = string.Empty;
        public string tip_producto { get; set; } = string.Empty;
        public string uni_producto { get; set; } = string.Empty;
        public string guia_proveedor { get; set; } = string.Empty;
        public string observacion { get; set; } = string.Empty;
        public CampaniaEntity? campania { get; set; }
        public ProductoEntity? producto { get; set; }
        public List<IngresoProductoImagenEntity>? ingreso_producto_imagenes { get; set; }
    }
}
