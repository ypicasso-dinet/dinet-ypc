using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Rol.Querys.Opciones
{
    public class OpcionesQueryValidator : AppBaseValidator<OpcionesQuery>
    {
        public OpcionesQueryValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de rol es requerido");
        }
    }
}
