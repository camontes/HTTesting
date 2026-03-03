using System.Globalization;

namespace HR_Platform.Application.ServicesInterfaces;

public interface ITimeFormatService
{
    string GetDateFormatMonthShort(DateTime date, string format, CultureInfo culture);
    string GetDateFormatMonthLarge(DateTime date, string format, CultureInfo culture);
    string GetDateTimeFormatMonthToltip(DateTime date, string format, CultureInfo culture);
    string GetDateTimeFormartForEvent(DateTime startDate, TimeSpan startTime, DateTime endDate, TimeSpan endTime, CultureInfo culture);
}
