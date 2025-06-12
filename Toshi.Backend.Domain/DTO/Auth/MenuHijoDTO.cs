namespace Toshi.Backend.Domain.DTO.Auth
{
    public class MenuHijoDTO
    {
        public string cod_menu { get; set; } = string.Empty;
        public string tit_menu { get; set; } = string.Empty;
        public string ico_menu { get; set; } = string.Empty;
        public string url_menu { get; set; } = string.Empty;
        public int ord_menu { get; set; }

        public bool? ind_create { get; set; }
        public bool? ind_read { get; set; }
        public bool? ind_update { get; set; }
        public bool? ind_delete { get; set; }
        public bool? ind_all { get; set; }
    }
}
