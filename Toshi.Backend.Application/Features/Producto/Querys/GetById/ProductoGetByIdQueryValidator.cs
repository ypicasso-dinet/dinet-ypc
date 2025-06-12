using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Producto.Querys.GetById
{
    public class ProductoGetByIdQueryValidator : AppBaseValidator<ProductoGetByIdQuery>
    {
        public ProductoGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
