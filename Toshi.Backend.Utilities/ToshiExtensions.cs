using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Toshi.Backend.Utilities
{
    public static class ToshiExtensions
    {
        public static bool IsValidEmail(this string? me)
        {
            try
            {
                var email = new System.Net.Mail.MailAddress(me ?? "");

                return email != null;
            }
            catch // (Exception ex)
            {
                return false;
            }
        }

        public static string? ToSpanish(this DateTime? value) => value == null ? null : value!.Value.ToString("dd/MM/yyyy");
        public static string? ToSpanish(this DateTime value) => value.ToString("dd/MM/yyyy");

        public static string? ToDate(this DateTime? value) => value == null ? null : value!.Value.ToString("yyyy-MM-dd");
        public static string? ToDate(this DateTime value) => value.ToString("yyyy-MM-dd");

        public static string? ToDateTime(this DateTime? value) => value == null ? null : value!.ToDate();
        public static string? ToDateTime(this DateTime value) => value.ToString("yyyy-MM-dd HH:mm:ss");

        public static string? ToTime(this DateTime? value) => value == null ? null : value!.ToTime();
        public static string? ToTime(this DateTime value) => value.ToString("HH:mm");

        public static string ToStatus(this bool? value)
        {
            return value == true ? "Activo" : value == false ? "Inactivo" : "---";
        }

        public static bool ToBool(this string value) => string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, @"(1|true|si|yes)", RegexOptions.IgnoreCase);

        public static DateTime? ToDate(this string? value)
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

        public static string ToTurno(this bool? value) => value == null ? "" : true ? "Día" : "Noche";
        public static string ToTurno(this bool value) => value == true ? "Día" : "Noche";
        
        public static int? ToInt(this string? value)
        {
            try
            {
                int output;

                if(int.TryParse(value, out output))
                {
                    return output;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
