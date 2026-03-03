using System.Text.RegularExpressions;

namespace HR_Platform.Domain.ValueObjects;

public partial record MailDomain
{
    private const string Pattern = @"^([@][a-z]+([.][a-z]+)+)$";

    private MailDomain(string value) => Value = value;

    public static MailDomain? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || !DomainRegex().IsMatch(value))
            return null;

        return new MailDomain(value);
    }

    public string Value { get; init; }

    [GeneratedRegex(Pattern)]
    private static partial Regex DomainRegex();
}

