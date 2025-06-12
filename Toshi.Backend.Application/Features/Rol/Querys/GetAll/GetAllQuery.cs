using MediatR;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.GetAll
{
    public class GetAllQuery : IRequest<List<RolItemDTO>>
    {
        public string? cod_rol { get; set; }
        public string? nom_rol { get; set; }
    }
}
