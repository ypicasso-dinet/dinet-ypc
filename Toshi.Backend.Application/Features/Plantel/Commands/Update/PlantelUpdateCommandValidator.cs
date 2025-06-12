using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Commands.Update
{
    public class PlantelUpdateCommandValidator : AppBaseValidator<PlantelUpdateCommand>
    {
        public PlantelUpdateCommandValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El ID de plantel es requerido");
            RuleFor(r => r.cod_plantel).Must(IsValidString).WithMessage("El código de plantel es requerido");
            RuleFor(r => r.nom_plantel).Must(IsValidString).WithMessage("El nombre de plantel es requerido");
            RuleFor(r => r.ind_interno).Must(IsValidBoolean).WithMessage("El indicador interno es requerido");
        }
    }
}
