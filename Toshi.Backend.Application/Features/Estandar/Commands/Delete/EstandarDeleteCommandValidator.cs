using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Delete
{
    public class EstandarDeleteCommandValidator : AppBaseValidator<EstandarDeleteCommand>
    {
        public EstandarDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
