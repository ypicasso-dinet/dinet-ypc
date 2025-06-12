using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Querys.UpsertParams
{
    public class UpsertParamsQueryValidator : AppBaseValidator<UpsertParamsQuery>
    {
        public UpsertParamsQueryValidator()
        {
            // RuleFor(r => r.cod_UpsertParams).Must(IsValidString).WithMessage("El código de UpsertParams es requerido");
        }
    }
}
