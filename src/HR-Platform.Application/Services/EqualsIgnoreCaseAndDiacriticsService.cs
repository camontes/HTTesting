using HR_Platform.Application.ServicesInterfaces;
using System.Globalization;
using System.Text;

namespace HR_Platform.Application.Services
{
    public class EqualsIgnoreCaseAndDiacriticsService : IEqualsIgnoreCaseAndDiacriticsService
    {
        public bool EqualsIgnoreCaseAndDiacritics(string str1, string str2)
        {
            string NormalizeString(string text) =>
                new string(text.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray())
                    .Normalize(NormalizationForm.FormC);

            return NormalizeString(str1.ToLower()) == NormalizeString(str2.ToLower());
        }
    }
}
