using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Create
{
    public class PlantelCreateCommandValidator : AppBaseValidator<PlantelCreateCommand>
    {
        public PlantelCreateCommandValidator()
        {
            RuleFor(r => r.cod_plantel).Must(IsValidString).WithMessage("El código de plantel es requerido");
            RuleFor(r => r.nom_plantel).Must(IsValidString).WithMessage("El nombre de plantel es requerido");
        }
    }
}
