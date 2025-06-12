using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Proveedor;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalCreate
{
    public class PersonalCreateCommand : AppBaseCommand, MediatR.IRequest<ProveedorPersonalResponseDTO>
    {
        // Request Properties
        public string? id_persona { get; set; }

        //public string? cod_personal { get; set; }
        public string? nom_personal { get; set; }
        public string? ape_paterno { get; set; }
        public string? ape_materno { get; set; }
        //public string? fec_nacimiento { get; set; }
        public string? tip_documento { get; set; }
        public string? num_documento { get; set; }
        //public string? per_email { get; set; }
        public string? num_telefono { get; set; }
        //public string? id_rol { get; set; }
        //public string? id_plantel { get; set; }
        public string? id_proveedor { get; set; }
        //public bool? ind_turno { get; set; }
    }
}
