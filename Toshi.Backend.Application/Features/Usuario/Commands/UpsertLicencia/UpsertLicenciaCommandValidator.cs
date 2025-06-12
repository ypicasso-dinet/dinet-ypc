using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Commands.UpsertLicencia
{
    public class UpsertLicenciaCommandValidator : AppBaseValidator<UpsertLicenciaCommand>
    {
        public UpsertLicenciaCommandValidator()
        {
            RuleFor(r => r.id_usuario).Must(IsValidString).WithMessage(ERR_USUARIO);
            RuleFor(r => r.tip_licencia).Must(IsValidString).WithMessage("El tipo de licencia es requerido");
            RuleFor(r => r.fec_desde).Must(IsValidDateAny).WithMessage("La fecha desde no tiene el formato YYYY-MM-DD");
            RuleFor(r => r.fec_hasta).Must(IsValidDateAny).WithMessage("La fecha hasta no tiene el formato YYYY-MM-DD");
            RuleFor(r => r.obs_licencia).Must(IsValidString).WithMessage("La observación es requerida");
        }
    }
}
