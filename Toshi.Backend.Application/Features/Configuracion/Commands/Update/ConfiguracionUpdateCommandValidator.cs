using FluentValidation;
using Toshi.Backend.Application.Features.Common;
using Toshi.Backend.Domain;

namespace Toshi.Backend.Application.Features.Configuracion.Commands.Update
{
    public class ConfiguracionUpdateCommandValidator : AppBaseValidator<ConfiguracionUpdateCommand>
    {
        public ConfiguracionUpdateCommandValidator()
        {
            RuleFor(r => r.id_config).Must(IsValidString).WithMessage("El ID de configuración es requerido");
            RuleFor(r => r.tip_config).Must(IsValidString).WithMessage("El tipo de configuración es requerido");
            RuleFor(r => r.id_detalle).Must(IsValidString).WithMessage("El ID de detalle es requerido");

            When(w => w.tip_config == Constants.CONFIG_LS, () =>
            {
                RuleFor(r => r.cod_detalle).Must(IsValidString).WithMessage("El codigo es requerido");
                RuleFor(r => r.nom_detalle).Must(IsValidString).WithMessage("El nombre es requerido");
            });

            When(w => w.tip_config == Constants.CONFIG_LR, () =>
            {
                RuleFor(r => r.min_value).Must(IsValidDecimal).WithMessage("El valor mínimo es requerido");
                RuleFor(r => r.max_value).Must(IsValidDecimal).WithMessage("El valor máximo es requerido");
            });

            When(w => w.tip_config == Constants.CONFIG_LC, () =>
            {
                RuleFor(r => r.des_email).Must(IsValidString).WithMessage("El destinatario es requerido");
                RuleFor(r => r.val_email)
                    .Must(IsValidString).WithMessage("El correo es requerido")
                    .Must(IsValidEmail).WithMessage("El correo no tiene el formato correcto");
            });

            RuleFor(r => r.ord_detalle).Must(IsValidInt).WithMessage("El orden  es requerido");
        }
    }
}
