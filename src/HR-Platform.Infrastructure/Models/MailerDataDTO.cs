namespace HR_Platform.Infrastructure.Models;
public class MailerDataDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? RecoverPasswordURL { get; set; }
    public string? LoginCodeURL { get; set; }
    public string? ProvisoryCodeURL { get; set; }
    public string? LoginPlatformUrl { get; set; }
    public string? NotificationTemplateURL { get; set; }
}
