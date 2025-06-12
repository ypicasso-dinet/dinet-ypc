using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Delete
{
    public class ConfiguracionDeleteCommandValidator : AppBaseValidator<ConfiguracionDeleteCommand>
    {
        public ConfiguracionDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de detalle es requerido");
        }
    }
}
