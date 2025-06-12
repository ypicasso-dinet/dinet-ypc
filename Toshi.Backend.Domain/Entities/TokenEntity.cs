namespace Toshi.Backend.Domain.Entities
{
    public partial class TokenEntity : BaseDomainModel
    {
        public int id_token { get; set; }
        public string token { get; set; } = string.Empty;
    }
}
