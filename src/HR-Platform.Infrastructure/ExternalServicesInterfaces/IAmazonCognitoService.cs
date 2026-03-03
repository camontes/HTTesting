namespace HR_Platform.Infrastructure.ExternalServicesInterfaces;

public interface IAmazonCognitoService
{
    Task<string> CognitoAuthentication(string email, string password);
    Task<bool> CognitoSignUp(string email, string password);
    Task<bool> CognitoAdminChangePassword(string email, string newPassword);
    Task<string> CognitoChangePassword(string email, string oldPassword, string newPassword);
}
