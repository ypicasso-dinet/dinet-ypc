using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Menu.Commands.Delete
{
    public class MenuDeleteCommandValidator : AppBaseValidator<MenuDeleteCommand>
    {
        public MenuDeleteCommandValidator()
        {
            // RuleFor(r => r.cod_Menu).Must(IsValidString).WithMessage("El código de Menu es requerido");
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
