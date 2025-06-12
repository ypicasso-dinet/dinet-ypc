using MediatR;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones
{
    public class RolSaveAccionesCommand : IRequest<string>
    {
        public string? id_rol { get; set; }
        public List<RolMenuDTO>? opciones { get; set; }
    }
}
