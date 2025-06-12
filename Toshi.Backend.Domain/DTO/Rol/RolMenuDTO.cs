namespace Toshi.Backend.Domain.DTO.Rol
{
    public class RolMenuDTO
    {
        public string? cod_menu { get; set; }
        public string? nom_menu { get; set; }
        public string? tit_opcion { get; set; }

        public bool? ind_select { get; set; }
        public bool? ind_create { get; set; }
        public bool? ind_update { get; set; }
        public bool? ind_delete { get; set; }
        public bool? ind_all { get; set; }
    }
}
