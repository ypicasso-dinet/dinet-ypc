using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Commands.SaveAcciones
{
    public class RolSaveAccionesCommandValidator : AppBaseValidator<RolSaveAccionesCommand>
    {
        public RolSaveAccionesCommandValidator()
        {
            RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El ID de rol es requerido");
            
            RuleFor(r => r.opciones)
                .NotNull().WithMessage("Las acciones son requeridas")
                .NotEmpty().WithMessage("Las acciones son requeridas");
        }
    }
}
