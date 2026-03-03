namespace HR_Platform.Application.Tags.Common;

public record TagNamesResponse(
    Guid CollaboratorTagId,
    string Name,
    string NameEnglish
);
