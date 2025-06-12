using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Delete
{
    public class IngresoProductoDeleteCommandValidator : AppBaseValidator<IngresoProductoDeleteCommand>
    {
        public IngresoProductoDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de ingreso de producto es requerido");
        }
    }
}
