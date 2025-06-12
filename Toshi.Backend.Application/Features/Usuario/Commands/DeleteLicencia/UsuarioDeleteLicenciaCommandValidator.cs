using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Commands.DeleteLicencia
{
    public class UsuarioDeleteLicenciaCommandValidator : AppBaseValidator<UsuarioDeleteLicenciaCommand>
    {
        public UsuarioDeleteLicenciaCommandValidator()
        {
            RuleFor(r => r.id_usuario).Must(IsValidString).WithMessage(ERR_USUARIO);
            RuleFor(r => r.id_licencia).Must(IsValidString).WithMessage("El ID de licencia es requerida");
        }
    }
}
