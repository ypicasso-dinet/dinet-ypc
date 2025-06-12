using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.GetById
{
    public class ProveedorGetByIdQueryValidator : AppBaseValidator<ProveedorGetByIdQuery>
    {
        public ProveedorGetByIdQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
