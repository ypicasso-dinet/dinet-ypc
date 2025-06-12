using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Querys.GetById
{
    public class PlantelGetByIdQueryValidator : AppBaseValidator<PlantelGetByIdQuery>
    {
        public PlantelGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
