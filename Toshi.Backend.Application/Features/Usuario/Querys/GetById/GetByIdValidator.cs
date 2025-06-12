using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Querys.GetById
{
    public class GetByIdValidator : AppBaseValidator<GetByIdQuery>
    {
        public GetByIdValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage(ERR_USUARIO);
        }
    }
}
