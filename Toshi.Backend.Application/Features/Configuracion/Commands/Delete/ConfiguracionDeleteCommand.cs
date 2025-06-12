using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Delete
{
    public class ConfiguracionDeleteCommand : AppBaseCommand, MediatR.IRequest<string>
    {
        // Request Properties
        public string? id { get; set; }
    }
}
