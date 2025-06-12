using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Querys.GetListParams
{
    public class GetListParamsQueryValidator : AppBaseValidator<GetListParamsQuery>
    {
        public GetListParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetListParams).Must(IsValidString).WithMessage("El código de GetListParams es requerido");
        }
    }
}
