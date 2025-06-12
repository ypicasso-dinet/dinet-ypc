using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetScreenParams
{
    public class GetSalidaScreenParamsQueryValidator : AppBaseValidator<GetSalidaScreenParamsQuery>
    {
        public GetSalidaScreenParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetScreenParams).Must(IsValidString).WithMessage("El código de GetScreenParams es requerido");
        }
    }
}
