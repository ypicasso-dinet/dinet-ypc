using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.UsersByRol
{
    public class PersonalByRolQueryValidator : AppBaseValidator<PersonalByRolQuery>
    {
        public PersonalByRolQueryValidator()
        {
            // RuleFor(r => r.cod_UsersByRol).Must(IsValidString).WithMessage("El código de UsersByRol es requerido");
        }
    }
}
