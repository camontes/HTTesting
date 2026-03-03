using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Application.Services
{
    public class CalculateTimeDifference : ICalculateTimeDifference
    {
        public string CalculateTimeDifferenceFunction(string typeName, string typeNameEnglish,DateTime fromDate)
        {
            TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

            TimeSpan difference = horaColombiana - fromDate;

            if (difference.TotalMinutes < 60)
            {
                return $"{typeName} hace {(int)difference.TotalMinutes} {((int)difference.TotalMinutes == 1 ? "minuto" : "minutos")}.{typeNameEnglish} {(int)difference.TotalMinutes} {((int)difference.TotalMinutes == 1 ? "minute" : "minutes")} ago.";
            }
            else if (difference.TotalHours < 24)
            {
                return $"{typeName} hace {(int)difference.TotalHours} {((int)difference.TotalHours == 1 ? "hora" : "horas")}.{typeNameEnglish} {(int)difference.TotalHours} {((int)difference.TotalHours == 1 ? "hour" : "hours")} ago";
            }
            else if (difference.TotalDays < 30)
            {
                return $"{typeName} hace {(int)difference.TotalDays} {((int)difference.TotalDays == 1 ? "día" : "días")}.{typeNameEnglish} {(int)difference.TotalDays} {((int)difference.TotalDays == 1 ? "day" : "days")} ago";
            }
            else if (difference.TotalDays < 365)
            {
                int months = (int)(difference.TotalDays / 30);
                return $"{typeName} hace {months} {(months == 1 ? "mes" : "meses")}.{typeNameEnglish} {months} {(months == 1 ? "month" : "months")} ago";
            }
            else
            {
                int years = (int)(difference.TotalDays / 365);
                return $"{typeName} hace {years} {(years == 1 ? "año" : "años")}.{typeNameEnglish} {years} {(years == 1 ? "year" : "years")} ago";
            }
        }
    }
}
