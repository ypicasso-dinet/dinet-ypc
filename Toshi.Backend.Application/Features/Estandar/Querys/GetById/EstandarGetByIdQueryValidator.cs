using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Estandar.Querys.GetById
{
    public class EstandarGetByIdQueryValidator : AppBaseValidator<EstandarGetByIdQuery>
    {
        public EstandarGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
