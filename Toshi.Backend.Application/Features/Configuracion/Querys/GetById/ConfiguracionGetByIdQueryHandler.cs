using MediatR;
using Toshi.Backend.Application.Contracts.Persistence;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.GetById
{
    public class ConfiguracionGetByIdQueryHandler : IRequestHandler<ConfiguracionGetByIdQuery, ConfiguracionDTO?>
    {
        protected readonly IConfiguracionRepository repository;

        public ConfiguracionGetByIdQueryHandler(IConfiguracionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ConfiguracionDTO?> Handle(ConfiguracionGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetById(request);
        }
    }
}
