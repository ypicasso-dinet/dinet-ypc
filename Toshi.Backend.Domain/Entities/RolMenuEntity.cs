namespace Toshi.Backend.Domain.Entities
{
    public partial class RolMenuEntity : BaseDomainModel
    {
        public int id_rol_menu { get; set; }
        public int id_rol { get; set; }
        public int id_menu { get; set; }
        public bool? ind_create { get; set; }
        public bool? ind_read { get; set; }
        public bool? ind_update { get; set; }
        public bool? ind_delete { get; set; }
        public bool? ind_all { get; set; }
        public MenuEntity? menu { get; set; }
        public RolEntity? rol { get; set; }
    }
}
