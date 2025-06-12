using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.GetAll
{
    public class ConfiguracionGetAllQueryHandler : IRequestHandler<ConfiguracionGetAllQuery, List<ConfiguracionItemDTO>>
    {
        protected readonly IConfiguracionRepository repository;

        public ConfiguracionGetAllQueryHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ConfiguracionItemDTO>> Handle(ConfiguracionGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAll(request);
        }
    }
}
