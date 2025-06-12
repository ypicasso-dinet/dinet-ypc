namespace Toshi.Backend.Domain.Entities
{
    public partial class CampaniaEntity : BaseDomainModel
    {
        public int id_campania { get; set; }
        public string gid_campania { get; set; } = string.Empty;
        public int id_plantel { get; set; }
        public int num_anio { get; set; }
        public int num_campania { get; set; }
        public string cod_campania { get; set; } = string.Empty;
        public DateTime fec_limpieza { get; set; }
        public DateTime? fec_ingreso { get; set; }
        public DateTime? fec_venta { get; set; }
        public DateTime? fec_cierre { get; set; }
        public int? can_ingreso { get; set; }
        public int? can_mortalidad { get; set; }
        public int? can_venta { get; set; }
        public int ind_proceso { get; set; }
        public PlantelEntity? plantel { get; set; }
        public List<EnvioDiarioEntity>? envio_diarios { get; set; }
        public List<IngresoPolloEntity>? ingreso_pollos { get; set; }
        public List<IngresoProductoEntity>? ingreso_productos { get; set; }
        public List<SalidaProductoEntity>? salida_productos { get; set; }
    }
}
