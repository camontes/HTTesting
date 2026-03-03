namespace HR_Platform.Application.BirthdayTemplateSettings.Common;

public record BirthdayTemplateSettingsResponse(
    bool IsDefaultTemplate,
    bool IsAllowSendPost,
    string CustomMessage,
    string CustomTemplateURL,
    string CustomTemplateName
);
