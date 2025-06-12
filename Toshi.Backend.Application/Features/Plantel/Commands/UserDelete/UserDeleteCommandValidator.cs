using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.UserDelete
{
    public class UserDeleteCommandValidator : AppBaseValidator<UserDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El ID de plantel es requerido");
            RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El ID de usuario es requerido");
            RuleFor(r => r.id_usuario).Must(IsValidString).WithMessage("El ID de rol es requerido");
        }
    }
}
