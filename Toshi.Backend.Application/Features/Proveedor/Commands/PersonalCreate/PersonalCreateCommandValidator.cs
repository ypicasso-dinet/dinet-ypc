using FluentValidation;
using System.Text.RegularExpressions;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.PersonalCreate
{
    public class PersonalCreateCommandValidator : AppBaseValidator<PersonalCreateCommand>
    {
        public PersonalCreateCommandValidator()
        {
            //RuleFor(r => r.cod_personal).Must(IsValidString).WithMessage("El código es requerido");
            RuleFor(r => r.nom_personal).Must(IsValidString).WithMessage("El nommbre es requerido");
            RuleFor(r => r.ape_paterno).Must(IsValidString).WithMessage("El apellido paterno es requerido");
            //RuleFor(r => r.ape_materno).Must(IsValidString).WithMessage("El apellido materno es requerido");

            //RuleFor(r => r.fec_nacimiento).Must(IsValidDateRequited).WithMessage("La fecha de nacimiento es requerido");
            //When(w => w.fec_nacimiento != null, () =>
            //{
            //    RuleFor(r => r.fec_nacimiento).Must(IsValidDateAny).WithMessage(string.Format(ERR_FECHA, "fecha de nacimiento"));
            //});

            RuleFor(r => r.tip_documento).Must(IsValidString).WithMessage("El tipo de documento es requerido");
            RuleFor(r => r.num_documento).Must(IsValidString).WithMessage("El número de documento es requerido");
            
            When(w => IsValidString(w.tip_documento), () =>
            {
                When(w => w.tip_documento == "DNI", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{8})$")).WithMessage("El DNI debe tener 8 números"); });
                When(w => w.tip_documento == "CEX", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{1,12})$")).WithMessage("El carne de extranjeria debe tener entre 1 y 12 números"); });
                When(w => w.tip_documento == "PASS", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{1,12})$")).WithMessage("El pasaporte debe tener entre 1 y 12 números"); });
            });

            //RuleFor(r => r.per_email).Must(IsValidString).WithMessage("El er_email es requerido");
            //When(w => IsValidString(w.per_email), () =>
            //{
            //    RuleFor(r => r.per_email).Must(IsValidEmail).WithMessage("El email no tiene el formato correcto");
            //});

            //RuleFor(r => r.num_telefono).Must(IsValidString).WithMessage("El teléfono es requerido");
            When(w => IsValidString(w.num_telefono), () =>
            {
                RuleFor(r => r.num_telefono).Must(value => Regex.IsMatch(value!, @"^(9\d{8})$")).WithMessage("El número de telefono debe tener 9 caracteres y comenzar en 9");
            });

            //RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El rol es requerido");
            //RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El plantel es requerido");
            RuleFor(r => r.id_proveedor).Must(IsValidString).WithMessage("El proveedor es requerido");
            //RuleFor(r => r.ind_turno).Must(IsValidBoolean).WithMessage("El turno es requerido");
        }
    }
}
