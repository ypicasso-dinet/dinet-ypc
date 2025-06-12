namespace Toshi.Backend.Domain.DTO.Menu
{
    public class MenuDTO
    {
        public string cod_menu { get; set; } = string.Empty;
        public string tit_menu { get; set; } = string.Empty;
        public string ico_menu { get; set; } = string.Empty;
        public string url_menu { get; set; } = string.Empty;
        public int ord_menu { get; set; }
        public List<MenuDTO>? items { get; set; }
    }
}
