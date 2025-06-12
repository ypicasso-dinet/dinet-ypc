using FluentValidation;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.IngresoPollo.Commands.Create
{
    public class IngresoPolloCreateCommandValidator : AppBaseValidator<IngresoPolloCreateCommand>
    {
        public IngresoPolloCreateCommandValidator()
        {
            RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El plantel es requerido");
            RuleFor(r => r.id_campania).Must(IsValidString).WithMessage("La campaña es requerida");
            RuleFor(r => r.fec_registro).Must(IsValidDateTimeRequited).WithMessage("La fecha de registro no tiene el formato YYYY-MM-DD HH:mm");

            RuleFor(r => r.num_galpon)
                .Must(IsValidInt).WithMessage("El galpón es requerido");
                //.InclusiveBetween(1, 8).WithMessage("El galpón indicado no se encuentra dentro de los configurados");

            RuleFor(r => r.cod_genero).Must(IsValidString).WithMessage("El género es requerido");

            RuleFor(r => r.can_ingreso)
                .Must(IsValidInt).WithMessage("La cantidad es requerida")
                .GreaterThan(0).WithMessage("La cantidad de ser mayor a cero");

            RuleFor(r => r.lot_procedencia).Must(IsValidString).WithMessage("El lote de procedencia es requerido");
            RuleFor(r => r.nom_procedencia).Must(IsValidString).WithMessage("La procedencia es requerida");
            RuleFor(r => r.num_guia).Must(IsValidString).WithMessage("El número de guía es requerido");
            RuleFor(r => r.cod_edad).Must(IsValidString).WithMessage("La edad es requerida");
            RuleFor(r => r.cod_lote).Must(IsValidString).WithMessage("El lote es requerido");
            RuleFor(r => r.cod_linea).Must(IsValidString).WithMessage("La línea es requerida");

            RuleFor(r => r.val_peso)
                .Must(IsValidDecimal).WithMessage("El peso es requerido")
                .GreaterThan(0).WithMessage("El peso de ser mayor a cero");

            RuleFor(r => r.can_muertos)
                .Must(IsValidInt).WithMessage("La cantidad de muertos es requerido")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad de ser mayor o igual a cero");

            //RuleFor(r => r.can_real).Must(IsValidInt).WithMessage("El can_real es requerido");
            RuleFor(r => r.num_vehiculo).Must(IsValidString).WithMessage("El número de vehículo es requerido");
            RuleFor(r => r.temp_cabina).Must(IsValidDecimal).WithMessage("La temperatura de cabina es requerida");
            RuleFor(r => r.hum_cabina).Must(IsValidDecimal).WithMessage("La humedad de cabina es requerida");
        }
    }
}
