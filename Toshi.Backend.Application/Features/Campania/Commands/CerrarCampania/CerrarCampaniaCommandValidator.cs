using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Commands.CerrarCampania
{
    public class CerrarCampaniaCommandValidator : AppBaseValidator<CerrarCampaniaCommand>
    {
        public CerrarCampaniaCommandValidator()
        {
            RuleFor(r => r.id_campania).Must(IsValidString).WithMessage("La campaña es requerida");
            RuleFor(r => r.fec_cierre).Must(IsValidDateRequired).WithMessage("La fecha de cierre es requerida y debe tener el formato YYYY-MM-DD");
        }
    }
}
