using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Offline.Queries.GetInitialInfo
{
    public class GetInitialInfoQueryValidator : AppBaseValidator<GetInitialInfoQuery>
    {
        public GetInitialInfoQueryValidator()
        {
            // RuleFor(r => r.cod_GetInitialInfo).Must(IsValidString).WithMessage("El código de GetInitialInfo es requerido");
        }
    }
}
