namespace HR_Platform.Application.BirthdayTemplateSettings.Common;

public record BirthdayCollaboratorsResponse(
    bool IsDefaultTemplate,
    bool IsAllowSendPost,
    string? CustomMessage,
    string? CustomTemplateURL,
    string? CustomTemplateName,
    List<CollaboratorBirthdayInfo> Collaborators
);

public record CollaboratorBirthdayInfo(
    string Name,
    string Photo,
    string Birthday,
    string BirthdayEnglish
);
