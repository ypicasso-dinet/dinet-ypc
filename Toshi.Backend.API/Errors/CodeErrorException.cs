namespace Toshi.Backend.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? detail { get; set; }
        public List<string>? errors { get; set; }

        public CodeErrorException(int codigoError, string? mensaje = null, string? detalle = null) : base(codigoError, mensaje)
        {
            detail = detalle;

            if (!string.IsNullOrEmpty(detalle))
            {
                var values = new System.Text.RegularExpressions.Regex(@"([a-zA-Z\.:\\]+?)\.cs:line\s\d+").Matches(detalle);

                if (values != null && values!.Count > 0)
                {
                    detail = string.Join("\r\n", values.Select(s => s.Value).ToArray());
                }
            }
        }
    }
}
