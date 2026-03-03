using System.Globalization;

namespace HR_Platform.Domain.ValueObjects;

public partial record TimeDate
{
    public readonly string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

    private TimeDate(string value) => Value = DateTime.ParseExact(value, formats, new CultureInfo("en-US"), DateTimeStyles.None);

    public static TimeDate? Create(string? value)
    {
       string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        if (!DateTime.TryParseExact(value, formats, new CultureInfo("en-US"), DateTimeStyles.None, out _))
            return null;

        return new TimeDate(value);
    }

    public DateTime Value { get; init; }
}

