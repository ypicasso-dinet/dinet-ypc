using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetScreenParams
{
    public class IngresoProductoGetScreenParamsQueryValidator : AppBaseValidator<IngresoProductoGetScreenParamsQuery>
    {
        public IngresoProductoGetScreenParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetScreenParams).Must(IsValidString).WithMessage("El código de GetScreenParams es requerido");
        }
    }
}
