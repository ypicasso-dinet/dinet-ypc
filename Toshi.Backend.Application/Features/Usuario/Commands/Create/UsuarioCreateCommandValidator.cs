using FluentValidation;
using System.Text.RegularExpressions;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Usuario.Commands.Create
{
    public class UsuarioCreateCommandValidator : AppBaseValidator<UsuarioCreateCommand>
    {
        public UsuarioCreateCommandValidator()
        {
            RuleFor(r => r.cod_usuario)
                .Must(IsValidString).WithMessage("El código es requerido")
                .Must(value => Regex.IsMatch(value ?? "", "^([a-zA-Z0-9_-]+)$", RegexOptions.IgnoreCase)).WithMessage("Para el código de usuario solo esta permitido letras, numeros, guión bajo ó guión medio");

            RuleFor(r => r.nom_usuario).Must(IsValidString).WithMessage("El nombre es requerido");
            RuleFor(r => r.ape_paterno).Must(IsValidString).WithMessage("El apellido paterno es requerido");

            When(w => w.fec_nacimiento != null, () =>
            {
                RuleFor(r => r.fec_nacimiento).Must(IsValidDateAny).WithMessage(string.Format(ERR_FECHA, "fecha de nacimiento"));
            });

            RuleFor(r => r.tip_documento).Must(IsValidString).WithMessage("El tipo de documento es requerido");
            RuleFor(r => r.num_documento).Must(IsValidString).WithMessage("El número de documento es requerido");

            When(w => IsValidString(w.usu_email), () =>
            {
                RuleFor(r => r.usu_email).Must(IsValidEmailToshi).WithMessage("El email no tiene el formato correcto y debe terminar en @gtoshi.com");
            });

            When(w => IsValidString(w.num_telefono), () =>
            {
                RuleFor(r => r.num_telefono).Must(value => Regex.IsMatch(value!, @"^(9\d{8})$")).WithMessage("El número de telefono debe tener 9 caracteres y comenzar en 9");
            });

            RuleFor(r => r.id_rol).Must(IsValidString).WithMessage("El rol es requerido");
            RuleFor(r => r.tip_usuario).Must(IsValidString).WithMessage("El tipo de usuario es requerido");
            RuleFor(r => r.ind_turno).Must(IsValidBoolean).WithMessage("El turno es requerido");

            When(w => IsValidString(w.tip_usuario), () =>
            {
                When(w => w.tip_usuario == "DNI", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{8})$")).WithMessage("El DNI debe tener 8 números"); });
                When(w => w.tip_usuario == "CEX", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{1,12})$")).WithMessage("El carne de extranjeria debe tener entre 1 y 12 números"); });
                When(w => w.tip_usuario == "PASS", () => { RuleFor(r => r.num_documento).Must(v => Regex.IsMatch(v ?? "", "^(\\d{1,12})$")).WithMessage("El pasaporte debe tener entre 1 y 12 números"); });
            });

            //When(w => w.tip_usuario == "PROV", () =>
            //{
            //    RuleFor(r => r.id_proveedor).Must(IsValidString).WithMessage("El proveedor es requerido");
            //});

            RuleFor(r => r.planteles)
                .NotNull().WithMessage("Se requiere por lo menos un plantel")
                .NotEmpty().WithMessage("Se requiere por lo menos un plantel");

            When(w => w.planteles != null && w.planteles.Count > 0, () =>
            {
                RuleForEach(f => f.planteles).ChildRules(cr =>
                {
                    cr.RuleFor(r => r.id_plantel).Must(IsValidString).WithMessage("El de ID de plantel es requerido");
                });
            });
        }
    }
}
