namespace Toshi.Backend.Domain.DTO.Common
{
    public class EmailSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PortNumber { get; set; }
        public bool EnableSSL { get; set; }
        public bool IsOfficeServer { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
