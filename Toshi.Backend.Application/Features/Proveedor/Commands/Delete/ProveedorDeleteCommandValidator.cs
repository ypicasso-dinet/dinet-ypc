using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Delete
{
    public class ProveedorDeleteCommandValidator : AppBaseValidator<ProveedorDeleteCommand>
    {
        public ProveedorDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID es requerido");
        }
    }
}
