using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.BuscarPersona
{
    public class BuscarPersonaCommandValidator : AppBaseValidator<BuscarPersonaCommand>
    {
        public BuscarPersonaCommandValidator()
        {
            RuleFor(r => r.tip_documento).Must(IsValidString).WithMessage("El tipo de documento es requerido");
            RuleFor(r => r.num_documento).Must(IsValidString).WithMessage("El número de documento es requerido");
        }
    }
}
