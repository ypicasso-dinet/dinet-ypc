using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetListParams
{
    public class GetSalidaListParamsQueryValidator : AppBaseValidator<GetSalidaListParamsQuery>
    {
        public GetSalidaListParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetListParams).Must(IsValidString).WithMessage("El código de GetListParams es requerido");
        }
    }
}
