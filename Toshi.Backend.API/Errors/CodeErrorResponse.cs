namespace Toshi.Backend.API.Errors
{
    public class CodeErrorResponse
    {
        public int statusCode { get; set; }
        public string? message { get; set; }

        public CodeErrorResponse(int codigoError, string? mensaje)
        {
            statusCode = codigoError;
            message = mensaje ?? GetDefaultMessageStatusCode(codigoError);
        }

        private string GetDefaultMessageStatusCode(int codigoError)
        {
            return codigoError switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontró el recurso solicitado",
                500 => "Se produjeron errores en el servidor",
                _ => string.Empty
            };
        }
    }
}
