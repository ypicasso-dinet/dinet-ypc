namespace Toshi.Backend.Domain.DTO.Auth
{
    public class AuthDTO
    {
        public string? id_usuario { get; set; }
        public string? cod_usuario { get; set; }
        public string? nom_usuario { get; set; }
        public string ape_paterno { get; set; } = string.Empty;
        public string ape_materno { get; set; } = string.Empty;
        public string? email { get; set; }
        public string? token { get; set; }
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
        
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        public string? num_telefono { get; set; }
        public string? fec_nacimiento { get; set; }
        public string? tip_usuario{ get; set; }
        public string? nom_proveedor { get; set; }

        public bool? ind_repassword { get; set; }
        public bool? ind_turno { get; set; }
        public string? nom_turno { get; set; }

        public List<AuthPlantelDTO>? planteles { get; set; }
        public List<MenuPadreDTO>? menus { get; set; }
    }
}
