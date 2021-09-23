namespace konatus.api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int HoursExpire { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
    }
}