using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Amazon;
using HR_Platform.Application.ServicesInterfaces;

namespace HR_Platform.Infrastructure.ExternalServices;

public class SecretsManagerAmazonService : ISecretsManagerAmazonService
{
    public string GetSecret(string secretName, string AWSRegion)
    {
        try
        {
            AmazonSecretsManagerClient client = new(RegionEndpoint.GetBySystemName(AWSRegion));

            GetSecretValueRequest secretConnectionStringRequest = new()
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT"
            };

            GetSecretValueResponse response = client.GetSecretValueAsync(secretConnectionStringRequest).Result;
            string secret = response.SecretString;

            return secret;
        }
        catch (AggregateException ae)
        {
            // Muestra el error real
            throw new Exception($"Error obteniendo secreto '{secretName}': {ae.InnerException?.Message}", ae);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error obteniendo secreto '{secretName}': {ex.Message}", ex);
        }
    }
}
