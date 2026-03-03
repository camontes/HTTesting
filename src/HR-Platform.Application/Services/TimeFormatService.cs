using HR_Platform.Application.ServicesInterfaces;
using System.Globalization;

namespace HR_Platform.Application.Services;

public class TimeFormatService : ITimeFormatService
{
    public string GetDateFormatMonthShort(DateTime date, string format, CultureInfo culture)
    {
        return date.ToString(format, culture).Replace(".", "");
    }

    public string GetDateFormatMonthLarge(DateTime date, string format, CultureInfo culture)
    {
        if(culture.Name == "es-CO")
            return date.ToString("dd", culture).Replace(".", "") + " de " + date.ToString("MMMM yyyy", culture).Replace(".", "");

        return date.ToString(format, culture).Replace(".", "");
    }

    public string GetDateTimeFormatMonthToltip(DateTime date, string format, CultureInfo culture)
    {
        return date.ToString(format, culture).Replace(".", "").Replace("AM", "a.m.").Replace("PM", "p.m.").Replace("am", "a.m.").Replace("a m", "a.m.").Replace("p m", "p.m.");
    }

    public string GetDateTimeFormartForEvent(DateTime startDate, TimeSpan startTime, DateTime endDate, TimeSpan endTime, CultureInfo culture)
    {
        DateTime startDateTime = startDate.Date + startTime;
        DateTime endDateTime = endDate.Date + endTime;

        string formattedDate = startDateTime.ToString("dddd, d 'de' MMMM", culture); // "martes, 12 de junio"
        string formattedStartTime = startDateTime.ToString("hh:mm tt", culture).ToLower(); // "11:30 a.m."
        string formattedEndTime = endDateTime.ToString("hh:mm tt", culture).ToLower(); // "12:00 p.m."

        return $"{formattedDate} {formattedStartTime} - {formattedEndTime}";
    }
}
