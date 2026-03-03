namespace HR_Platform.Application.ServicesInterfaces;

public interface ISecretsManagerAmazonService
{
    string GetSecret(string secretName, string AWSRegion);
}
