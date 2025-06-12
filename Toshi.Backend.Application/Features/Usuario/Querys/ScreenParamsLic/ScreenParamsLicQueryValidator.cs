using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Querys.ScreenParamsLic
{
    public class ScreenParamsLicQueryValidator : AppBaseValidator<ScreenParamsLicQuery>
    {
        public ScreenParamsLicQueryValidator()
        {
            // RuleFor(r => r.cod_ScreenParamsLic).Must(IsValidString).WithMessage("El código de ScreenParamsLic es requerido");
        }
    }
}
