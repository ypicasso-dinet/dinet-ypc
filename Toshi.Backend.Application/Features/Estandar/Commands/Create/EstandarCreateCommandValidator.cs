using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Estandar.Commands.Create
{
    public class EstandarCreateCommandValidator : AppBaseValidator<EstandarCreateCommand>
    {
        public EstandarCreateCommandValidator()
        {
            RuleFor(r => r.cod_lote).Must(IsValidString).WithMessage("El lote es requerido");
            RuleFor(r => r.val_edad).Must(IsValidInt).WithMessage("La edad es requerida");
            RuleFor(r => r.cod_sexo).Must(IsValidString).WithMessage("El sexo es requerido");
            RuleFor(r => r.val_estandar).Must(IsValidInt).WithMessage("La cantidad estandar es requerida");
            RuleFor(r => r.val_peso).Must(IsValidInt).WithMessage("El peso es requerido");
            RuleFor(r => r.val_consumo).Must(IsValidInt).WithMessage("El consumo es requerido");
            RuleFor(r => r.val_mortalidad).Must(IsValidDecimal).WithMessage("La mortalidad es requerida");
            RuleFor(r => r.val_conversion).Must(IsValidDecimal).WithMessage("La conversion es requerida");
            RuleFor(r => r.val_eficiencia).Must(IsValidDecimal).WithMessage("La eficiencia productiva es requerida");
        }
    }
}
