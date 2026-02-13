public static class BrazilTime
{
    private static readonly TimeZoneInfo BrazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

    public static DateTime Now()
    {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, BrazilTimeZone);
    }

    public static DateTimeOffset NowOffset()
    {
        return TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, BrazilTimeZone);
    }
}