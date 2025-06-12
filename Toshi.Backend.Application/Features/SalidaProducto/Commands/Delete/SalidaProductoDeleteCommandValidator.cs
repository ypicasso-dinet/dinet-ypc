using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Delete
{
    public class SalidaProductoDeleteCommandValidator : AppBaseValidator<SalidaProductoDeleteCommand>
    {
        public SalidaProductoDeleteCommandValidator()
        {
            // RuleFor(r => r.cod_SalidaProducto).Must(IsValidString).WithMessage("El código de SalidaProducto es requerido");
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
