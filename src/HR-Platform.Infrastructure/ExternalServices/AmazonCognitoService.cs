using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HR_Platform.Infrastructure.ExternalServices;

public class AmazonCognitoService(
    ISecretsManagerAmazonService secretsManagerAmazonService,
    IConfiguration configuration
    ) : IAmazonCognitoService
{
    private readonly ISecretsManagerAmazonService _secretsManagerAmazonService = secretsManagerAmazonService;

    private readonly IConfiguration _configuration = configuration;

    public async Task<string> CognitoAuthentication(string email, string password)
    {
        string Region = _configuration["Region"]!;
        string CognitoSecret = _configuration["CognitoSecret"]!;

        CognitoSecretDTO cognitoSecretObject = JsonConvert.DeserializeObject<CognitoSecretDTO>(_secretsManagerAmazonService.GetSecret(CognitoSecret, Region))!;

        AmazonCognitoIdentityProviderClient provider = new(new AnonymousAWSCredentials(), Amazon.RegionEndpoint.USEast1);
        CognitoUserPool userPool = new(cognitoSecretObject.PoolId, cognitoSecretObject.AppClientId, provider);

        CognitoUser user = new(email, cognitoSecretObject.AppClientId, userPool, provider);

        try
        {
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
            {
                Password = password
            });

            return authResponse.AuthenticationResult.AccessToken;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public async Task<bool> CognitoSignUp(string email, string password)
    {
        string Region = _configuration["Region"]!;
        string CognitoSecret = _configuration["CognitoSecret"]!;

        CognitoSecretDTO cognitoSecretObject = JsonConvert.DeserializeObject<CognitoSecretDTO>(_secretsManagerAmazonService.GetSecret(CognitoSecret, Region))!;

        BasicAWSCredentials awsCredentials = new(cognitoSecretObject.AccessKey, cognitoSecretObject.SecretKey);

        AmazonCognitoIdentityProviderClient provider = new(awsCredentials, Amazon.RegionEndpoint.USEast1);

        AdminCreateUserRequest adminCreateUserRequest = new()
        {
            Username = email,
            TemporaryPassword = password,
            UserPoolId = cognitoSecretObject.PoolId,
            DesiredDeliveryMediums = [],
            MessageAction = "SUPPRESS",

            UserAttributes =
                [
                    new ()
                    {
                        Name = "email",
                        Value = email
                    }
                ]
        };

        try
        {
            AdminCreateUserResponse adminCreateUserResponse = await provider.AdminCreateUserAsync(adminCreateUserRequest);

            try
            {
                AdminUpdateUserAttributesRequest adminUpdateUserAttributes = new()
                {
                    Username = email,
                    UserPoolId = cognitoSecretObject.PoolId,

                    UserAttributes =
                    [
                        new()
                        {
                            Name = "email_verified",
                            Value = "True"
                        }
                    ]
                };

                AdminUpdateUserAttributesResponse adminUpdateUserAttributesResponse = await provider.AdminUpdateUserAttributesAsync(adminUpdateUserAttributes);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> CognitoAdminChangePassword(string email, string newPassword)
    {
        string Region = _configuration["Region"]!;
        string CognitoSecret = _configuration["CognitoSecret"]!;

        CognitoSecretDTO cognitoSecretObject = JsonConvert.DeserializeObject<CognitoSecretDTO>(_secretsManagerAmazonService.GetSecret(CognitoSecret, Region))!;

        BasicAWSCredentials awsCredentials = new(cognitoSecretObject.AccessKey, cognitoSecretObject.SecretKey);

        AmazonCognitoIdentityProviderClient provider = new(awsCredentials, Amazon.RegionEndpoint.USEast1);

        try
        {
            AdminSetUserPasswordRequest adminSetUserPasswordRequest = new()
            {
                Username = email,
                Password = newPassword,
                UserPoolId = cognitoSecretObject.PoolId,
                Permanent = true
            };

            AdminSetUserPasswordResponse adminSetUserPasswordResponse = await provider.AdminSetUserPasswordAsync(adminSetUserPasswordRequest);
        }
        catch (NotAuthorizedException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<string> CognitoChangePassword(string email, string oldPassword, string newPassword)
    {
        string Region = _configuration["Region"]!;
        string CognitoSecret = _configuration["CognitoSecret"]!;

        CognitoSecretDTO cognitoSecretObject = JsonConvert.DeserializeObject<CognitoSecretDTO>(_secretsManagerAmazonService.GetSecret(CognitoSecret, Region))!;

        BasicAWSCredentials awsCredentials = new (cognitoSecretObject.AccessKey, cognitoSecretObject.SecretKey);

        AmazonCognitoIdentityProviderClient provider = new (awsCredentials, Amazon.RegionEndpoint.USEast1);

        try
        {
            AdminSetUserPasswordRequest adminSetUserPasswordRequest = new()
            {
                Username = email,
                Password = newPassword,
                UserPoolId = cognitoSecretObject.PoolId,
                Permanent = true
            };

            AdminSetUserPasswordResponse adminSetUserPasswordResponse = await provider.AdminSetUserPasswordAsync(adminSetUserPasswordRequest);
        }
        catch (NotAuthorizedException)
        {
            return "Not Authorized";
        }
        catch (InvalidPasswordException ex)
        {
            return ex.Message;
        }
        catch (Exception)
        {
            return "Change Failed";
        }

        return "Change Successfully";
    }
}
