using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalDelete
{
    public class PersonalDeleteCommandValidator : AppBaseValidator<PersonalDeleteCommand>
    {
        public PersonalDeleteCommandValidator()
        {
            RuleFor(r => r.id_proveedor).Must(IsValidString).WithMessage("El ID de proveedor es requerido");
            //RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El ID de rol es requerido");
            RuleFor(r => r.id_personal).Must(IsValidString).WithMessage("El ID de personal es requerido");
        }
    }
}
