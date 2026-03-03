using System.Net.Mail;

namespace HR_Platform.Domain.ValueObjects;

public partial record Email
{
    private Email(string value) => Value = value;

    public static Email? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new Email(string.Empty);

        try
        {
            MailAddress mailAddress = new(value);

            return new Email(value);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    public string Value { get; init; }
}
