using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.Create
{
    public class RolCreateCommandValidator : AppBaseValidator<RolCreateCommand>
    {
        public RolCreateCommandValidator()
        {
            RuleFor(r => r.cod_rol).Must(IsValidString).WithMessage("El código de rol es requerido");
            RuleFor(r => r.nom_rol).Must(IsValidString).WithMessage("El nombre de rol es requerido");
        }
    }
}
