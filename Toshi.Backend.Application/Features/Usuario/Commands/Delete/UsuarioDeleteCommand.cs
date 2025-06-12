using MediatR;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Delete
{
    public class UsuarioDeleteCommand : IRequest<string>
    {
        public string? id { get; set; }
    }
}
