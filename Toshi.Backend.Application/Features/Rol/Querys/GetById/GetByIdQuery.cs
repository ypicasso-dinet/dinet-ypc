using MediatR;
using Toshi.Backend.Domain.DTO.Rol;

namespace Toshi.Backend.Application.Features.Rol.Querys.GetById
{
    public class GetByIdQuery : IRequest<RolDTO>
    {
        public string? id { get; set; }

        public GetByIdQuery(string? id)
        {
            this.id = id;
        }
    }
}
