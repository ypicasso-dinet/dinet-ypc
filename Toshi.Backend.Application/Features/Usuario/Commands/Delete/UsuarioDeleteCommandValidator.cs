using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Delete
{
    public class UsuarioDeleteCommandValidator : AppBaseValidator<UsuarioDeleteCommand>
    {
        public UsuarioDeleteCommandValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de usuario es requerido");
        }
    }
}
