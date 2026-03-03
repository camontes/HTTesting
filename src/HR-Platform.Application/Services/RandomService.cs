using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Application.Services;

public class RandomService : IRandomService
{
    readonly Random random = new((int)DateTime.Now.Ticks & 0x0000FFFF);

    public string GetRandomLoginCode()
    {
        string randomCode = string.Empty;

        List<string> strings = GenerateRandomCode(6, 1);

        if (strings != null && strings.Count > 0)
            randomCode = strings[0];

        return randomCode;
    }

    public string GetRandomRecoveryCode()
    {
        string randomCode = string.Empty;

        List<string> strings = GenerateRandomCode(6, 1);

        if (strings != null && strings.Count > 0)
            randomCode = strings[0];

        return randomCode;
    }

    public string GenerateRandomPasswordNineLetters()
    {
        string validUpperChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        string validLowerChars = "abcdefghijklmnopqrstuvwxyz";
        string validNumbers = "0123456789";

        char[] superLowerString = new char[3];
        char[] superUpperString = new char[3];
        char[] superNumber = new char[3];

        for (int i = 0; i < 3; i++)
        {
            superLowerString[i] = validLowerChars[random.Next(0, validLowerChars.Length)];
        }

        for (int i = 0; i < 3; i++)
        {
            superUpperString[i] = validUpperChars[random.Next(0, validUpperChars.Length)];
        }

        for (int i = 0; i < 3; i++)
        {
            superNumber[i] = validNumbers[random.Next(0, validNumbers.Length)];
        }

        string superLowerStringConverted = new(superLowerString);
        string superUpperStringConverted = new(superUpperString);
        string superNumberConverted = new(superNumber);
        return superUpperStringConverted + superLowerStringConverted + superNumberConverted + "*";
    }

    public List<string> GenerateRandomCode(int stringLength, int numberStrings)
    {
        List<string> strings = [];

        string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        char[] superString = new char[stringLength * numberStrings];

        for (int i = 0; i < stringLength * numberStrings; i++)
        {
            superString[i] = validChars[random.Next(0, validChars.Length)];
        }

        string superStringConverted = new(superString);

        for (int i = 0; i < numberStrings; i++)
        {
            strings.Add(superStringConverted.Substring(i * stringLength, stringLength));
        }

        return strings;
    }
}
