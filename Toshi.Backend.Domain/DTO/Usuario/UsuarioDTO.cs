namespace Toshi.Backend.Domain.DTO.Usuario
{
    public class UsuarioDTO
    {
        public string? id_usuario { get; set; }
        public string? cod_usuario { get; set; }
        public string? nom_usuario { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? fec_nacimiento { get; set; }
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        public string? usu_email { get; set; }
        public string? num_telefono { get; set; }
        public string? pwd_usuario { get; set; }
        public string? fec_cese { get; set; }
        public string? obs_cese { get; set; }
        public string? id_rol { get; set; }
        public string? tip_usuario { get; set; }
        public string? id_proveedor { get; set; }

        public string? nom_estado { get; set; }
        public string? date_nacimiento { get; set; }

        public bool? ind_turno { get; set; }

        public List<UsuarioPlantelDTO>? planteles { get; set; }
        public List<UsuarioLicenciaDTO>? licencias { get; set; }
    }
}
