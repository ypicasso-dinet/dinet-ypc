using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetAll
{
    public class GetAllQueryValidator : AppBaseValidator<GetAllQuery>
    {
        public GetAllQueryValidator()
        {
            // RuleFor(r => r.cod_GetAll).Must(IsValidString).WithMessage("El código de GetAll es requerido");
        }
    }
}
