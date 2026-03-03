using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorsResponse(
    Guid Id,

    Guid CompanyId,
    Guid RoleId,
    int DocumentTypeId,
    string OtherDocumentType,

    Guid AssignationId,
    int CollaboratorStateId,

    string Document,

    string DocumentTypeName,
    string DocumentTypeNameEnglish,

    string AssignationName,
    string AssignationNameEnglish,

    string BusinessEmail,
    string PersonalEmail,

    string Name,
    string Initials,

    string RoleName,
    string RoleNameEnglish,

    string PhotoURL,
    string PhotoName,

    string PhoneNumber,

    bool IsSuspended,
    bool ShowNewFeatures,

    string SuspensionReason,

    DateTime EntranceDate,

    string EntranceDateFormatMonthShort,
    string EntranceDateFormatMonthShortEnglish,

    string EntranceDateFormatSlash,
    string EntranceDateFormatSlashEnglish,

    DateTime EditionDate,

    string EditionDateFormatMonthLarge,
    string EditionDateFormatMonthLargeEnglish,

    string EditionDateTimeFormatMonthToltip,
    string EditionDateTimeFormatMonthToltipEnglish
);
