using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Auth.Command.Retrieve
{
    public class RetrieveCommandValidator : AppBaseValidator<RetrieveCommand>
    {
        public RetrieveCommandValidator()
        {
            //RuleFor(r => r.cod_usuario).Must(IsValidEmail).WithMessage("La cuenta de correo no tiene el formato correcto");
            RuleFor(r => r.cod_usuario).Must(IsValidString).WithMessage("La cuenta de usuario es requerida");
        }
    }
}
