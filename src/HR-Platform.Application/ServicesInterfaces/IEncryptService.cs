namespace HR_Platform.Application.ServicesInterfaces;

public interface IEncryptService
{
    string EncryptString(string plainText);
    string DecryptString(string cipherText);
}
