using FluentValidation;
using System.Text.RegularExpressions;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Cuenta.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AppBaseValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(r => r.old_password).Must(IsValidString).WithMessage("La contraseña actual es requerida");
            RuleFor(r => r.new_password).Must(IsValidString).WithMessage("La nueva contraseña es requerida");
            RuleFor(r => r.con_password).Must(IsValidString).WithMessage("La confirmació de contraseña es requerida");

            When(w => !string.IsNullOrEmpty(w.new_password) && !string.IsNullOrEmpty(w.con_password), () =>
            {
                RuleFor(r => r.new_password).Equal(r => r.con_password).WithMessage("Las contraseñas no coinciden");

                RuleFor(r => r.new_password)
                    .Must(value => Regex.IsMatch(value!, "[a-z]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos una minúscula")
                    .Must(value => Regex.IsMatch(value!, "[A-Z]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos una mayúscula")
                    .Must(value => Regex.IsMatch(value!, "[0-9]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos un número")
                    .Must(value => Regex.IsMatch(value!, "[^a-zA-Z0-9]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos un caracter especial")
                    .Must(value => Regex.IsMatch(value!, ".{6,}")).WithMessage("La nueva contraseña debe tener mínimo 6 digitos")
                    ;

                RuleFor(r => r.con_password)
                    .Must(value => Regex.IsMatch(value!, "[a-z]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos una minúscula")
                    .Must(value => Regex.IsMatch(value!, "[A-Z]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos una mayúscula")
                    .Must(value => Regex.IsMatch(value!, "[0-9]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos un número")
                    .Must(value => Regex.IsMatch(value!, "[^a-zA-Z0-9]+")).WithMessage("La nueva contraseña debe tener 1 por lo menos un caracter especial")
                    .Must(value => Regex.IsMatch(value!, ".{6,}")).WithMessage("La nueva contraseña debe tener mínimo 6 digitos")
                    ;
            });
        }
    }
}
