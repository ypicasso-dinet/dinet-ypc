namespace Toshi.Backend.Domain.Entities
{
    public partial class ContraseniaEntity : BaseDomainModel
    {
        public int id_contrasenia { get; set; }
        public int id_usuario { get; set; }
        public string pwd_usuario { get; set; } = string.Empty;
        public UsuarioEntity? usuario { get; set; }
    }
}
