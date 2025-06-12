using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Update
{
    public class RolUpdateCommandValidator : AppBaseValidator<RolUpdateCommand>
    {
        public RolUpdateCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
            RuleFor(r => r.cod_rol).Must(IsValidString).WithMessage("El código es requerido");
            RuleFor(r => r.nom_rol).Must(IsValidString).WithMessage("El nombre es requerido");
        }
    }
}
