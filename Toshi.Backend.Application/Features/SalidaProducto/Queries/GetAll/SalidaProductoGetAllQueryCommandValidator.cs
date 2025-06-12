using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Queries.GetAll
{
    public class SalidaProductoGetAllQueryValidator : AppBaseValidator<SalidaProductoGetAllQuery>
    {
        public SalidaProductoGetAllQueryValidator()
        {
            // Validations if is needed
            RuleFor(r => r.fec_desde).Must(IsValidDate).WithMessage(ERR_DESDE);
            RuleFor(r => r.fec_hasta).Must(IsValidDate).WithMessage(ERR_HASTA);
        }
    }
}
