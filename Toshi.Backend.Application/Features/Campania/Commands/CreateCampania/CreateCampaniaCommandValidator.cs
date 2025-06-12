using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Commands.CreateCampania
{
    public class CreateCampaniaCommandValidator : AppBaseValidator<CreateCampaniaCommand>
    {
        public CreateCampaniaCommandValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El plantel es requerido");
            RuleFor(r => r.ind_actual).Must(IsValidBoolean).WithMessage("El indicador de año actual es requerido");
            RuleFor(r => r.fec_limpieza).Must(IsValidDate).WithMessage("La fecha de limpieza es requerida");
        }
    }
}
