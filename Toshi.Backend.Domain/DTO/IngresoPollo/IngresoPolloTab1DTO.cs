namespace Toshi.Backend.Domain.DTO.IngresoPollo
{
    public class IngresoPolloTab1DTO
    {
        public string? id_ingreso_pollo { get; set; }
        public string? id_campania { get; set; }
        public string? fec_registro { get; set; }
        public int? num_galpon { get; set; }
        public string? cod_genero { get; set; }
        public int? can_ingreso { get; set; }
        public string? lot_procedencia { get; set; }
        public string? nom_procedencia { get; set; }
        public string? num_guia { get; set; }
        public string? cod_edad { get; set; }
        public string? cod_lote { get; set; }
        public string? cod_linea { get; set; }
        public decimal? val_peso { get; set; }
        public int? can_muertos { get; set; }
        public int? can_real { get; set; }
        public string? num_vehiculo { get; set; }
        public decimal? temp_cabina { get; set; }
        public decimal? hum_cabina { get; set; }


        public string? nom_plantel { get; set; }
        public string? nom_campania { get; set; }
        public string? nom_lote { get; set; }
        public string? nom_genero { get; set; }
        public string? nom_linea { get; set; }
        public string? dia_mes { get; set; }

        public int? tmp_real { get; set; }
        public string? hora_registro { get; set; }
        public string? nom_estado { get; set; }
        public bool? cod_estado { get; set; }
        public string? cod_plantel { get; set; }

        public bool allow_update { get; set; }
        public bool allow_delete { get; set; }
        public bool? ind_enviado { get; set; }
        public string? nom_enviado { get; set; }

        public string? color { get; set; }
        public List<string> imagenes { get; set; } = new();
    }
}
