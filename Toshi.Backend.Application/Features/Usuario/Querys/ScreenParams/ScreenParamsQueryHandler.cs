using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Usuario;

namespace Toshi.Backend.Application.Features.Usuario.Querys.ScreenParams
{
    public class ScreenParamsQueryHandler : MediatR.IRequestHandler<ScreenParamsQuery, UsuarioScreenParamsDTO>
    {
        protected readonly IUsuarioRepository repository;

        public ScreenParamsQueryHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UsuarioScreenParamsDTO> Handle(ScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParams(request);
        }
    }
}
