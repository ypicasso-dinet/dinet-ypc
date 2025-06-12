namespace Toshi.Backend.Domain.DTO.Common
{
    public class StatusResponse : StatusResponse<string>
    {
        public StatusResponse() : base()
        {
        }

        public StatusResponse(string? message, string? data) : base(message, data)
        {
        }
    }

    public class StatusResponse<D>
    {
        public StatusResponse()
        {
        }

        public StatusResponse(string? message, D? data)
        {
            this.message = message;
            this.data = data;
        }

        public string? message { get; set; }
        public D? data { get; set; }
    }
}
