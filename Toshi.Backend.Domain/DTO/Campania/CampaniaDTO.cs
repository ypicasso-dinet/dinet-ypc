namespace Toshi.Backend.Domain.DTO.Campania
{
    public class CampaniaDTO
    {
        public string? id_campania { get; set; }
        public string? nom_plantel { get; set; }
        public int num_anio { get; set; }
        public string? cod_campania { get; set; }
        public string? fec_limpieza { get; set; }
        public string? fec_ingreso { get; set; }
        public string? fec_venta { get; set; }
        public string? fec_cierre { get; set; }
        public int? can_ingreso { get; set; }
        public int? can_mortalidad { get; set; }
        public int? can_venta { get; set; }

        public int ind_proceso { get; set; }
        public string? nom_proceso { get; set; }
        public int val_saldo => (can_ingreso ?? 0) - (can_mortalidad ?? 0);
        public int val_final => val_saldo - (can_venta ?? 0);

        public bool allow_update { get; set; }
    }
}
