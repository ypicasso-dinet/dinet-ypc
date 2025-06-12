using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Auth.Command.Signin
{
    public class SigninCommandValidator : AppBaseValidator<SigninCommand>
    {
        public SigninCommandValidator()
        {
            RuleFor(r => r.username).Must(IsValidString).WithMessage("El usuario es requerido");
            RuleFor(r => r.password).Must(IsValidString).WithMessage("La contraseña es requerida");
        }
    }
}
