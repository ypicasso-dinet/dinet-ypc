using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoProducto.Commands.Update
{
    public class IngresoProductoUpdateCommandValidator : AppBaseValidator<IngresoProductoUpdateCommand>
    {
        public IngresoProductoUpdateCommandValidator()
        {
            RuleFor(r => r.id_ingreso_producto).Must(IsValidString).WithMessage("El ID ingreso es requerido");
            //------------------------------------------------------------------------------------------------------------------------------------------
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El plantel es requerido");
            RuleFor(r => r.id_campania).Must(IsValidString).WithMessage("La campaña es requerida");
            RuleFor(r => r.fec_registro).Must(IsValidDateTimeRequited).WithMessage("La fecha de registro no tiene el formato YYYY-MM-DD HH:mm");

            RuleFor(r => r.id_producto).Must(IsValidString).WithMessage("El producto es requerido");

            RuleFor(r => r.can_ingreso)
                    .Must(IsValidDecimal).WithMessage("La cantidad es requerida");
        }
    }
}
