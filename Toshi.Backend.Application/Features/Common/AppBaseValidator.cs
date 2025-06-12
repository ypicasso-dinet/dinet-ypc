using FluentValidation;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Toshi.Backend.Application.Features.Common
{
    public abstract class AppBaseValidator<T> : AbstractValidator<T>
    {
        protected const string ERR_USUARIO = "El ID de usuario es requerido";
        protected const string ERR_FECHA = "La fecha {0} no tiene el formato YYYY-MM-DD";
        protected const string ERR_DESDE = "La fecha de inicio no tiene el formato YYYY-MM-DD";
        protected const string ERR_HASTA = "La fecha de termino no tiene el formato YYYY-MM-DD";
        protected const string EMAIL_REGEX = "^[a-zA-Z0-9._%+-]+@gtoshi\\.com$";

        protected bool IsValidDate(string? value)
            => string.IsNullOrEmpty(value) ? true : Regex.IsMatch(value, @"^(\d{4}-\d{2}-\d{2})$");

        protected bool IsValidDateRequired(string? value)
            => string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, @"^(\d{4}-\d{2}-\d{2})$");

        protected bool IsValidDateTimeRequited(string? value)
            => string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, @"^(\d{4}-\d{2}-\d{2} \d{1,2}:\d{1,2})$");

        protected bool IsValidDateAny(string? value)
            => string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, @"^(\d{4}-\d{2}-\d{2}(\s|T\d{2}:\d{2}:\d{2}(\.\d{1,3}(z|Z)?)?)?)$");

        protected bool IsValidTime(string? value)
            => string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, @"^(\d{1,2}:\d{1,2})$", RegexOptions.IgnoreCase);

        protected bool IsValidString(string? value)
            => !string.IsNullOrEmpty(value);

        protected bool IsValidInt(int? value)
            => value == null ? false : value!.Value >= 0;

        protected bool IsValidDecimal(decimal? value)
            => value == null ? false : value!.Value >= 0;

        protected bool IsValidBoolean(bool? value)
            => value != null;

        protected bool IsValidEmailToshi(string? value) => string.IsNullOrEmpty(value)
            ? false
            : new Regex(EMAIL_REGEX, RegexOptions.IgnoreCase).IsMatch(value);

        protected bool IsValidEmail(string? value) => string.IsNullOrEmpty(value)
            ? false
            : new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase).IsMatch(value);

        protected DateTime? GetDate(string? value)
        {
            try
            {
                DateTime fecha;

                var formatos = new string[]
                {
                    "yyyy-MM-dd",
                    "yyyy-MM-dd HH:mm",
                    "yyyy-MM-ddTHH:mm",
                    "yyyy-MM-dd HH:mm:ss",
                    "yyyy-MM-ddTHH:mm:ss",
                    "yyyy-MM-dd HH:mm:ss.fff",
                    "yyyy-MM-ddTHH:mm:ss.fff",
                    "yyyy-MM-dd HH:mm:ss.fffZ",
                    "yyyy-MM-ddTHH:mm:ss.fffZ",
                };

                if (DateTime.TryParseExact(value ?? "", formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    return fecha;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            return null;
        }
    }
}
