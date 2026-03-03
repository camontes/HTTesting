namespace HR_Platform.Infrastructure.Models;

public class PasswordMailerDTO
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
