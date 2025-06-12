using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Querys.GetListParams
{
    public class IngresoProductoGetListParamsQueryValidator : AppBaseValidator<IngresoProductoGetListParamsQuery>
    {
        public IngresoProductoGetListParamsQueryValidator()
        {
            // RuleFor(r => r.cod_GetListParams).Must(IsValidString).WithMessage("El código de GetListParams es requerido");
        }
    }
}
