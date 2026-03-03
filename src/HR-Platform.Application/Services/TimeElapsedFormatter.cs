using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Application.Services;

public class TimeElapsedFormatter : ITimeElapsedFormatter
{
    public string GetTimeElapsed(DateTime fromDate)
    {
        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);
        var timeSpan = horaColombiana - fromDate;

        if (timeSpan.TotalMinutes < 60)
        {
            return $"{(int)timeSpan.TotalMinutes} min.{(int)timeSpan.TotalMinutes} min";
        }
        else if (timeSpan.TotalHours < 24)
        {
            return $"{(int)timeSpan.TotalHours} h.{(int)timeSpan.TotalHours} h";
        }
        else if (timeSpan.TotalDays < 30)
        {
            return $"{(int)timeSpan.TotalDays} d.{(int)timeSpan.TotalDays} d";
        }
        else if (timeSpan.TotalDays < 365)
        {
            int months = (int)(timeSpan.TotalDays / 30);
            return $"{months} m.{months} m";
        }
        else
        {
            int years = (int)(timeSpan.TotalDays / 365);
            return $"{years} a.{years} y";
        }
    }
}
