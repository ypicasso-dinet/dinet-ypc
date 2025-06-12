namespace Toshi.Backend.Domain.Entities
{
    public partial class IngresoPolloImagenEntity : BaseDomainModel
    {
        public int id_ingreso_pollo_imagen { get; set; }
        public string gid_ingreso_pollo_imagen { get; set; } = string.Empty;
        public int id_ingreso_pollo { get; set; }
        public string nom_imagen { get; set; } = string.Empty;
        public string url_imagen { get; set; } = string.Empty;
        public IngresoPolloEntity? ingreso_pollo { get; set; }
    }
}
