using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.ScreenParams
{
    public class ScreenParamsQueryHandler : MediatR.IRequestHandler<ScreenParamsQuery, ConfiguracionScreenParamsDTO>
    {
        protected readonly IConfiguracionRepository repository;

        public ScreenParamsQueryHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ConfiguracionScreenParamsDTO> Handle(ScreenParamsQuery request, CancellationToken cancellationToken)
        {
            return await repository.ScreenParams(request);
        }
    }
}
