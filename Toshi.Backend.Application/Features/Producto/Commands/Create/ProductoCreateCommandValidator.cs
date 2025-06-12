using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Commands.Create
{
    public class ProductoCreateCommandValidator : AppBaseValidator<ProductoCreateCommand>
    {
        public ProductoCreateCommandValidator()
        {
            // RuleFor(r => r.cod_Producto).Must(IsValidString).WithMessage("El código de Producto es requerido");
        }
    }
}
