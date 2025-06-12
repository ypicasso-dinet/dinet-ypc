using MediatR;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain.DTO.Configuracion;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.GetById
{
    public class ConfiguracionGetByIdQuery : AppBaseCommand, IRequest<ConfiguracionDTO?>
    {
        public string? id { get; set; }
    }
}
