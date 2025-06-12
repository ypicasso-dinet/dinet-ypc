using FluentValidation;
using System.Text.RegularExpressions;
using Toshi.Backend.Application.Features.Common;

namespace Toshi.Backend.Application.Features.Proveedor.Commands.Create
{
    public class ProveedorCreateCommandValidator : AppBaseValidator<ProveedorCreateCommand>
    {
        public ProveedorCreateCommandValidator()
        {
            RuleFor(r => r.ruc_proveedor).Must(IsValidString).WithMessage("El RUC es requerido");
            RuleFor(r => r.nom_proveedor).Must(IsValidString).WithMessage("El nombre es requerido");

            When(r => !string.IsNullOrEmpty(r.ruc_proveedor), () =>
            {
                RuleFor(r => r.ruc_proveedor)
                    .Must(value => value!.Length == 11).WithMessage("La longitud del RUC debe de ser de 11 caracteres")
                    .Must(value => new Regex(@"^((20|10)\d{9})$", RegexOptions.IgnoreCase).IsMatch(value!)).WithMessage("El ruc debe comenzar con 20 o 10");
            });
        }
    }
}
