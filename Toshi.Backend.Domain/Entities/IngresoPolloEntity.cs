namespace Toshi.Backend.Domain.Entities
{
    public partial class IngresoPolloEntity : BaseDomainModel
    {
        public int id_ingreso_pollo { get; set; }
        public string gid_ingreso_pollo { get; set; } = string.Empty;
        public int id_campania { get; set; }
        public DateTime fec_registro { get; set; }
        public int num_galpon { get; set; }
        public string cod_genero { get; set; } = string.Empty;
        public int can_ingreso { get; set; }
        public string lot_procedencia { get; set; } = string.Empty;
        public string nom_procedencia { get; set; } = string.Empty;
        public string num_guia { get; set; } = string.Empty;
        public string cod_edad { get; set; } = string.Empty;
        public string cod_lote { get; set; } = string.Empty;
        public string cod_linea { get; set; } = string.Empty;
        public decimal val_peso { get; set; }
        public int can_muertos { get; set; }
        public int can_real { get; set; }
        public string num_vehiculo { get; set; } = string.Empty;
        public decimal temp_cabina { get; set; }
        public decimal hum_cabina { get; set; }
        public CampaniaEntity? campania { get; set; }
        public List<IngresoPolloImagenEntity>? ingreso_pollo_imagenes { get; set; }
    }
}
