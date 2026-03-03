using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BirthdayTemplateSettings.Update;

public record UpdateBaseBirthdayTemplateSettingsCommand(
    bool IsDefaultTemplate,
    bool IsAllowSendPost,
    string? TemplateMessage,
    IFormFile? Template
) : IRequest<ErrorOr<bool>>;
