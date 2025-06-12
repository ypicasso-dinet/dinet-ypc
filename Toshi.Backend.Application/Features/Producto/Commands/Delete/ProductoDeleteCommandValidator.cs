using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Commands.Delete
{
    public class ProductoDeleteCommandValidator : AppBaseValidator<ProductoDeleteCommand>
    {
        public ProductoDeleteCommandValidator()
        {
            // RuleFor(r => r.cod_Producto).Must(IsValidString).WithMessage("El código de Producto es requerido");
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
