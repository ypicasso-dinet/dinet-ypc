using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Menu.Commands.Update
{
    public class MenuUpdateCommandValidator : AppBaseValidator<MenuUpdateCommand>
    {
        public MenuUpdateCommandValidator()
        {
            // RuleFor(r => r.cod_Menu).Must(IsValidString).WithMessage("El código de Menu es requerido");
        }
    }
}
