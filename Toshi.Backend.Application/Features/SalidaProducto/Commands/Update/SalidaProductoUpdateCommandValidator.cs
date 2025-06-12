using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.SalidaProducto.Commands.Update
{
    public class SalidaProductoUpdateCommandValidator : AppBaseValidator<SalidaProductoUpdateCommand>
    {
        public SalidaProductoUpdateCommandValidator()
        {
            RuleFor(r => r.id_salida_producto).Must(IsValidString).WithMessage("El códigoID de salida es requerida");

            RuleFor(r => r.id_campania).Must(IsValidString).WithMessage("La campaña es requerida");
            RuleFor(r => r.id_producto).Must(IsValidString).WithMessage("El producto es requerido");

            RuleFor(r => r.can_salida)
                .NotNull().WithMessage("La cantidad es requerida")
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

            When(r => r.imagenes != null && r.imagenes.Count > 0, () =>
            {
                RuleForEach(fe => fe.imagenes).ChildRules(cr =>
                {
                    cr.RuleFor(r => r.url_imagen).Must(IsValidString).WithMessage("La URL de imagen es requerida");
                });
            });
        }
    }
}
