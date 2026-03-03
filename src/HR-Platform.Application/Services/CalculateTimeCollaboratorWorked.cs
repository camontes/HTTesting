using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Application.Services
{
    public class CalculateTimeCollaboratorWorked : ICalculateTimeCollaboratorWorked
    {
        public string CalculateTimeCollaboratorWorkedFunction(DateTime startDate)
        {
            TimeSpan difference = DateTime.Now - startDate;

            // Si la diferencia es menor a un mes, muestra los días
            if (difference.TotalDays < 30)
            {
                return $"{(int)difference.TotalDays} días.{(int)difference.TotalDays} days";
            }
            // Si la diferencia es menor a un año, muestra los meses
            else if (difference.TotalDays < 365)
            {
                int months = (int)(difference.TotalDays / 30);
                return $"{months} {(months == 1 ? "mes" : "meses")}.{months} {(months == 1 ? "month" : "months")}";
            }
            // Si la diferencia es mayor a un año, muestra los años
            else
            {
                int years = (int)(difference.TotalDays / 365);
                return $"{years} {(years == 1 ? "año" : "años")}.{years} {(years == 1 ? "year" : "years")}";
            }
        }
    }
}
