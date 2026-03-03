namespace HR_Platform.Application.Collaborators.Common;

public record LanguagesCollaboratorResponse
(
    string LanguageId,
    int? LanguageNameId,
    int? LanguageLevelId,
    string? LanguageName,
    string? LanguageNameEnglish,
    string? LanguageLevel,
    string? LanguageLevelEnglish,
    string? OtherName
);