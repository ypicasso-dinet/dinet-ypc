using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Delete
{
    public class RolDeleteCommandValidator:AppBaseValidator<RolDeleteCommand>
    {
        public RolDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
