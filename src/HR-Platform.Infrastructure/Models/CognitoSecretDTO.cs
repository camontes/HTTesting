namespace HR_Platform.Infrastructure.Models;
public class CognitoSecretDTO
{
    public string? PoolId { get; set; }
    public string? AppClientId { get; set; }
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
}
