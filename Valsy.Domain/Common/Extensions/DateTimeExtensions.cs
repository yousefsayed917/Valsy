namespace Valsy.Domain.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime CorrectTimeZone(this DateTime date)
        {
            TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), egyptTimeZone);
        }
        public static DateTime FormattedDate(this DateTime date)
        {
            return DateTime.Parse(date.ToString("yyyy-MM-ddTHH:mm:ss")); // add 2 hour between local time and server
        }
    }
}
