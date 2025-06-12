using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Queries.ScreenParams
{
    public class ScreenParamsQueryValidator : AppBaseValidator<ScreenParamsQuery>
    {
        public ScreenParamsQueryValidator()
        {
            // RuleFor(r => r.cod_ScreenParams).Must(IsValidString).WithMessage("El código de ScreenParams es requerido");
        }
    }
}
