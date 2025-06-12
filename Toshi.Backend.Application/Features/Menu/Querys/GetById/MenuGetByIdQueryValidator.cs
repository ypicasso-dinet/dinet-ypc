using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Menu.Querys.GetById
{
    public class MenuGetByIdQueryValidator : AppBaseValidator<MenuGetByIdQuery>
    {
        public MenuGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
