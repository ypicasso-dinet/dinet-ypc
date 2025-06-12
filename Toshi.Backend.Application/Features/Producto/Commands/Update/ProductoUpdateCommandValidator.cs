using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Commands.Update
{
    public class ProductoUpdateCommandValidator : AppBaseValidator<ProductoUpdateCommand>
    {
        public ProductoUpdateCommandValidator()
        {
            // RuleFor(r => r.cod_Producto).Must(IsValidString).WithMessage("El código de Producto es requerido");
        }
    }
}
