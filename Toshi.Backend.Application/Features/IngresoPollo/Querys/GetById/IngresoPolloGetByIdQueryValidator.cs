using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetById
{
    public class IngresoPolloGetByIdQueryValidator : AppBaseValidator<IngresoPolloGetByIdQuery>
    {
        public IngresoPolloGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
