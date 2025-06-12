using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Configuracion.Querys.GetById
{
    public class ConfiguracionGetByIdQueryValidator : AppBaseValidator<ConfiguracionGetByIdQuery>
    {
        public ConfiguracionGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
