using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Campania.Queries.GetById
{
    public class GetByIdQueryValidator : AppBaseValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(r => r.id).Must(IsValidString).WithMessage("El ID de campaña es requerido");
        }
    }
}
