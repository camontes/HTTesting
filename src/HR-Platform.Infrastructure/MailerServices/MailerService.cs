using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Infrastructure.MailerServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HR_Platform.Infrastructure.MailerServices;

public class MailerService(
    ISecretsManagerAmazonService secretsManagerAmazonService,
    IConfiguration configuration,
    IHttpClientFactory httpClient) : IMailerService
{
    private readonly ISecretsManagerAmazonService _secretsManagerAmazonService = secretsManagerAmazonService;

    private readonly IConfiguration _configuration = configuration;

    private readonly IHttpClientFactory _httpClient = httpClient;

    public async Task SendLoginCodeMail(TokenMailerDTO tokenMailer)
    {
        string Region = _configuration["Region"]!;
        string mailerDataSecret = _configuration["MailerDataSecret"]!;

        MailerDataDTO mailerData = JsonConvert.DeserializeObject<MailerDataDTO>(_secretsManagerAmazonService.GetSecret(mailerDataSecret, Region))!;

        string year = DateTime.Now.Year.ToString();

        MimeMessage email = new()
        {
            Sender = MailboxAddress.Parse(mailerData.Email),
            Subject = tokenMailer.Subject,
        };

        email.To.Add(MailboxAddress.Parse(tokenMailer.To));

        BodyBuilder builder = new();

        string stringUri = mailerData != null && mailerData.LoginCodeURL != null ? mailerData.LoginCodeURL : string.Empty;
        Uri uri = new(stringUri);

        HttpClient httpClient = _httpClient.CreateClient();

        Stream stream = await httpClient.GetStreamAsync(uri);
        StreamReader reader = new(stream);

        builder.HtmlBody = reader.ReadToEnd().Replace("passwordcode", tokenMailer.Token).Replace("currentYear", year);

        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new();

        if (mailerData != null)
        {
            await smtp.ConnectAsync(mailerData.Host, mailerData.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailerData.Email, mailerData.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }

    public async Task SendBenefitDeletedNotification(List<BenefitDeletedMailerDTO> benefitsDeleted)
    {
        // Configuración y secretos
        string Region = _configuration["Region"]!;
        string mailerDataSecret = _configuration["MailerDataSecret"]!;

        MailerDataDTO mailerData = JsonConvert.DeserializeObject<MailerDataDTO>(
            _secretsManagerAmazonService.GetSecret(mailerDataSecret, Region))!;

        string year = DateTime.Now.Year.ToString();

        // Obtener la plantilla HTML
        string stringUri = mailerData != null && mailerData.NotificationTemplateURL != null
                            ? mailerData.NotificationTemplateURL
                            : string.Empty;

        Uri uri = new(stringUri);
        HttpClient httpClient = _httpClient.CreateClient();

        Stream stream = await httpClient.GetStreamAsync(uri);
        StreamReader reader = new(stream);
        string templateHtml = await reader.ReadToEndAsync();


        foreach (BenefitDeletedMailerDTO emailAddress in benefitsDeleted)
        {
            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(mailerData != null ? mailerData.Email : string.Empty),
                Subject = emailAddress.Subject,
            };

            email.To.Add(MailboxAddress.Parse(emailAddress.To));
            
            string subtitleMessage = $"El beneficio <b>{emailAddress.BenefitName}</b> ha sido eliminado";

            BodyBuilder builder = new();

            builder.HtmlBody = templateHtml
            .Replace("subtitulo", subtitleMessage)
            .Replace("message", emailAddress.Message)
            .Replace("currentYear", year);

            email.Body = builder.ToMessageBody();

            // Enviar el correo
            using SmtpClient smtp = new();
            if (mailerData != null)
            {
                await smtp.ConnectAsync(mailerData.Host, mailerData.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailerData.Email, mailerData.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }


    public async Task ResendEmailInvitation(PasswordMailerDTO passwordMailer)
    {
        string Region = _configuration["Region"]!;
        string mailerDataSecret = _configuration["MailerDataSecret"]!;

        MailerDataDTO mailerData = JsonConvert.DeserializeObject<MailerDataDTO>(_secretsManagerAmazonService.GetSecret(mailerDataSecret, Region))!;

        string year = DateTime.Now.Year.ToString();

        MimeMessage email = new()
        {
            Sender = MailboxAddress.Parse(mailerData.Email),
            Subject = passwordMailer.Subject,
        };

        email.To.Add(MailboxAddress.Parse(passwordMailer.To));

        BodyBuilder builder = new();

        string stringUri = mailerData != null && mailerData.ProvisoryCodeURL != null ? mailerData.ProvisoryCodeURL : string.Empty;
        Uri uri = new(stringUri);

        HttpClient httpClient = _httpClient.CreateClient();

        Stream stream = await httpClient.GetStreamAsync(uri);
        StreamReader reader = new(stream);

        string IsLoginPlatform = mailerData is not null && mailerData.LoginPlatformUrl is not null ? mailerData.LoginPlatformUrl : string.Empty;

        string logInLink = $"<a href=\"{IsLoginPlatform}\" target=\"_blank\">INGRESAR</a>";

        builder.HtmlBody = reader.ReadToEnd()
            .Replace("randompassword", passwordMailer.Password)
            .Replace("loginemail", passwordMailer.Email)
            .Replace("currentYear", year)
            .Replace("INGRESAR", logInLink);

        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new();

        if (mailerData != null)
        {
            await smtp.ConnectAsync(mailerData.Host, mailerData.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailerData.Email, mailerData.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }

    public async Task SendRecoveryPasswordMail(TokenMailerDTO tokenMailer)
    {
        string Region = _configuration["Region"]!;
        string mailerDataSecret = _configuration["MailerDataSecret"]!;

        MailerDataDTO mailerData = JsonConvert.DeserializeObject<MailerDataDTO>(_secretsManagerAmazonService.GetSecret(mailerDataSecret, Region))!;

        string year = DateTime.Now.Year.ToString();

        MimeMessage email = new()
        {
            Sender = MailboxAddress.Parse(mailerData.Email),
            Subject = tokenMailer.Subject,
        };

        email.To.Add(MailboxAddress.Parse(tokenMailer.To));

        BodyBuilder builder = new();

        string stringUri = mailerData != null && mailerData.RecoverPasswordURL != null ? mailerData.RecoverPasswordURL : string.Empty;
        Uri uri = new(stringUri);

        HttpClient httpClient = _httpClient.CreateClient();

        Stream stream = await httpClient.GetStreamAsync(uri);
        StreamReader reader = new(stream);

        builder.HtmlBody = reader.ReadToEnd().Replace("passwordcode", tokenMailer.Token).Replace("currentYear", year);

        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new();

        if (mailerData != null)
        {
            await smtp.ConnectAsync(mailerData.Host, mailerData.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailerData.Email, mailerData.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
