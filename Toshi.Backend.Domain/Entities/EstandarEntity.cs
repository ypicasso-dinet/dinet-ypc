namespace Toshi.Backend.Domain.Entities
{
    public partial class EstandarEntity : BaseDomainModel
    {
        public int id_estandar { get; set; }
        public string gid_estandar { get; set; } = string.Empty;
        public string? cod_carde { get; set; }
        public string cod_lote { get; set; } = string.Empty;
        public int val_edad { get; set; }
        public string cod_sexo { get; set; } = string.Empty;
        public int val_estandar { get; set; }
        public int val_peso { get; set; }
        public int val_consumo { get; set; }
        public decimal val_mortalidad { get; set; }
        public decimal val_conversion { get; set; }
        public decimal val_eficiencia { get; set; }
    }
}
