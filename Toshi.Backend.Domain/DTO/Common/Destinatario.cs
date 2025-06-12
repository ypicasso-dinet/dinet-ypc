namespace Toshi.Backend.Domain.DTO.Common
{
    public class Destinatario
    {
        public string? destino { get; set; }
        public string? correo { get; set; }

        public bool IsTO => destino == "TO";
        public bool IsCC => destino == "CC";
        public bool IsCO => destino == "CO";
    }
}
