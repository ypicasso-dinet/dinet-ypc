using MediatR;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Querys.GetById
{
    public class GetByIdQuery : IRequest<UsuarioDTO>
    {
        public string? id { get; set; }
    }
}
