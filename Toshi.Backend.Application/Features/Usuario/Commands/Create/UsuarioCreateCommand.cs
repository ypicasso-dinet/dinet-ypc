using MediatR;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Create
{
    public class UsuarioCreateCommand : IRequest<UsuarioCreateResponseDTO>
    {
        public string? cod_usuario { get; set; }
        public string? nom_usuario { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        public string? fec_nacimiento { get; set; }
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        public string? usu_email { get; set; }
        public string? num_telefono { get; set; }
        public string? id_rol { get; set; }
        public string? tip_usuario { get; set; }
        public string? id_proveedor { get; set; }
        public bool? ind_turno { get; set; }
        public List<UsuarioPlantelDTO>? planteles { get; set; }
    }
}
