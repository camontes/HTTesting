using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BirthdayTemplateSettings.Update;

public record UpdateBirthdayTemplateSettingsCommand(
    Guid CompanyId,
    bool IsDefaultTemplate,
    bool IsAllowSendPost,
    string? CustomMessage,
    string? CustomTemplateURL,
    string? CustomTemplateName
) : IRequest<ErrorOr<bool>>;
