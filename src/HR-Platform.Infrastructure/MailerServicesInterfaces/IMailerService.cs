using HR_Platform.Infrastructure.Models;

namespace HR_Platform.Infrastructure.MailerServicesInterfaces;

public interface IMailerService
{
    Task SendLoginCodeMail(TokenMailerDTO tokenMailer);
    Task SendBenefitDeletedNotification(List<BenefitDeletedMailerDTO> benefitsDeleted);
    Task ResendEmailInvitation(PasswordMailerDTO passwordMailer);
    Task SendRecoveryPasswordMail(TokenMailerDTO tokenMailer);
}
