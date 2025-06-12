using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Delete
{
    public class PlantelDeleteCommandValidator : AppBaseValidator<PlantelDeleteCommand>
    {
        public PlantelDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
