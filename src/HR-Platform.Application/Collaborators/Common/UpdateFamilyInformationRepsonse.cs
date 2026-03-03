namespace HR_Platform.Application.Collaborators.Common;

public record UpdateFamilyInformationResponse
(
    Guid Id,

    string Document,

    int DocumentTypeId,
    string OtherDocumentType,
    string DocumentTypeName,
    string DocumentTypeNameEnglish,

    string BusinessEmail,

    string Name,
    string Initials,

    string PhotoURL,

    string EditionDateFormatMonthShort,
    string EditionDateFormatMonthShortEnglish
);
