using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Menu.Commands.Create
{
    public class MenuCreateCommandValidator : AppBaseValidator<MenuCreateCommand>
    {
        public MenuCreateCommandValidator()
        {
            // RuleFor(r => r.cod_Menu).Must(IsValidString).WithMessage("El código de Menu es requerido");
        }
    }
}
