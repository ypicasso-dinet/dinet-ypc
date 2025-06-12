using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetById
{
    public class IngresoProductoGetByIdValidator : AppBaseValidator<IngresoProductoGetByIdQuery>
    {
        public IngresoProductoGetByIdValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
