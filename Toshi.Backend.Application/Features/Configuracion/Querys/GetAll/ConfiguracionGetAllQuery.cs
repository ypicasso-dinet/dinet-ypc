using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.GetAll
{
    public class ConfiguracionGetAllQuery : AppBaseCommand, IRequest<List<ConfiguracionItemDTO>>
    {
        // Request Properties
    }
}
