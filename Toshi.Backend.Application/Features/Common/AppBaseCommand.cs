using System.Text.Json.Serialization;

namespace Toshi.Backend.Application.Features.Common
{
    public class AppBaseCommand
    {
        [JsonIgnore]
        public string? UserCode { get; set; }
    }
}
