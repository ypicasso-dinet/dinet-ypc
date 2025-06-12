using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Querys.ScreenParams
{
    public class ScreenParamsQuery : MediatR.IRequest<UsuarioScreenParamsDTO>
    {
        // Request Properties
        public bool? upsert { get; set; }
    }
}
