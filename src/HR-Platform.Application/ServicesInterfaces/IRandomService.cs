namespace HR_Platform.Application.ServicesInterfaces;

public interface IRandomService
{
    string GetRandomLoginCode();
    string GetRandomRecoveryCode();
    string GenerateRandomPasswordNineLetters();
    List<string> GenerateRandomCode(int stringLength, int numberStrings);
}
