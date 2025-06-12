using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.UserAppend
{
    public class UserAppendCommandValidator : AppBaseValidator<UserAppendCommand>
    {
        public UserAppendCommandValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El ID de plantel es requerido");
            RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El ID de usuario es requerido");
            RuleFor(r => r.usuarios)
                .NotNull().WithMessage("Los usuarios son requeridos")
                .NotEmpty().WithMessage("Los usuarios son requeridos");

            When(w => w.usuarios != null && w.usuarios.Count > 0, () =>
            {
                RuleForEach(f => f.usuarios).ChildRules(cr =>
                {
                    cr.RuleFor(r => r.id_usuario).Must(IsValidString).WithMessage("El ID de usuario es requerido");
                });
            });
        }
    }
}
