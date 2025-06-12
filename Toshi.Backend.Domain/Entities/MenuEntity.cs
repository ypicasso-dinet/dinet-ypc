namespace Toshi.Backend.Domain.Entities
{
    public partial class MenuEntity : BaseDomainModel
    {
        public int id_menu { get; set; }
        public int? id_padre { get; set; }
        public string cod_menu { get; set; } = string.Empty;
        public string tit_menu { get; set; } = string.Empty;
        public string ico_menu { get; set; } = string.Empty;
        public string url_menu { get; set; } = string.Empty;
        public int ord_menu { get; set; }
        public int ind_entorno { get; set; }
        public List<RolMenuEntity>? rol_menus { get; set; }
    }
}
