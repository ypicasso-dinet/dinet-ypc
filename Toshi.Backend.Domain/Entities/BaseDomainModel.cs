namespace Toshi.Backend.Domain.Entities
{
    public class BaseDomainModel
    {
        public bool? cod_estado { get; set; }
        public string? usu_insert { get; set; }
        public string? usu_update { get; set; }

        public DateTime? fec_insert { get; set; }
        public DateTime? fec_update { get; set; }
        

        public static DateTime GetNow()
        {
            DateTime serverTime = DateTime.UtcNow;
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, "SA Pacific Standard Time");
            DateTime newDatetime = DateTime.SpecifyKind(localTime, DateTimeKind.Utc);

            return newDatetime;
        }
    }
}
