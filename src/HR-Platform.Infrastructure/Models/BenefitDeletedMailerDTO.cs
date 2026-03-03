namespace HR_Platform.Infrastructure.Models;

public class BenefitDeletedMailerDTO
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? BenefitName { get; set; }
    public string? Message { get; set; }
}
