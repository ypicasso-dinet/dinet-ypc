using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Plantel.Querys.ListParams
{
    public class ListParamsQueryValidator : AppBaseValidator<ListParamsQuery>
    {
        public ListParamsQueryValidator()
        {
            // RuleFor(r => r.cod_ListParams).Must(IsValidString).WithMessage("El código de ListParams es requerido");
        }
    }
}
