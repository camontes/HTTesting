namespace HR_Platform.Infrastructure.Models;

public class TokenMailerDTO
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Token { get; set; }
}
