using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetById
{
    public class SalidaProductoGetByIdQueryValidator : AppBaseValidator<SalidaProductoGetByIdQuery>
    {
        public SalidaProductoGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
