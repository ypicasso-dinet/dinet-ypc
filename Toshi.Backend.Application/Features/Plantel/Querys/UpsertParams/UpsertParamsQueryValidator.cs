using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Querys.UpsertParams
{
    public class UpsertParamsQueryValidator : AppBaseValidator<UpsertParamsQuery>
    {
        public UpsertParamsQueryValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El ID de plantel es requerido");
        }
    }
}
