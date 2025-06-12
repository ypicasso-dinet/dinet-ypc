using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia
{
    public class UpsertLicenciaCommand : MediatR.IRequest<UsuarioLicenciaResponseDTO>
    {
        // Request Properties
        public string? id_usuario { get; set; }
        public string? id_licencia { get; set; }
        public string? tip_licencia { get; set; }
        public string? fec_desde { get; set; }
        public string? fec_hasta { get; set; }
        public string? obs_licencia { get; set; }
    }
}
