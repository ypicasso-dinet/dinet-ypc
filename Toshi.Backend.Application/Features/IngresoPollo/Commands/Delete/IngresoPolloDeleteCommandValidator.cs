using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Delete
{
    public class IngresoPolloDeleteCommandValidator : AppBaseValidator<IngresoPolloDeleteCommand>
    {
        public IngresoPolloDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de ingreso de pollo es requerido");
        }
    }
}
