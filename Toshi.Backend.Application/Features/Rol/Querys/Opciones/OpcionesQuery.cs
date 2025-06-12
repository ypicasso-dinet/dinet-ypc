using MediatR;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.Opciones
{
    public class OpcionesQuery : IRequest<List<RolMenuDTO>>
    {
        public string? id { get; set; }

        public OpcionesQuery(string? id)
        {
            this.id = id;
        }
    }
}
