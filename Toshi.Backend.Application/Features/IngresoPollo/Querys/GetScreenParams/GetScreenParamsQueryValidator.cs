using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetScreenParams
{
    public class GetScreenParamsQueryValidator : AppBaseValidator<GetScreenParamsQuery>
    {
        public GetScreenParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetScreenParams).Must(IsValidString).WithMessage("El código de GetScreenParams es requerido");
        }
    }
}
