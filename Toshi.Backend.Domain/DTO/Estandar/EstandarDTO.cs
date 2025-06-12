namespace Toshi.Backend.Domain.DTO.Estandar
{
    public class EstandarDTO
    {
        public string? id_estandar { get; set; }
        public string? cod_carde { get; set; }
        public string? cod_lote { get; set; }
        public int val_edad { get; set; }
        public string? cod_sexo { get; set; }
        public int? val_estandar { get; set; }
        public int? val_peso { get; set; }
        public int? val_consumo { get; set; }
        public decimal? val_mortalidad { get; set; }
        public decimal? val_conversion { get; set; }
        public decimal? val_eficiencia { get; set; }

        public string? nom_estado { get; set; }
    }
}
