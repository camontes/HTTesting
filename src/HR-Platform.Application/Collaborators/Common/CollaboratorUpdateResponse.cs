namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorUpdateResponse(
    Guid Id,

    Guid CompanyId,
    Guid RoleId,
    Guid PensionId,
    Guid SeveranceBenefitId,
    Guid EducationalLevelId,
    Guid ProfessionalAdvice,

    int DocumentTypeId,
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

    //string PhoneNumber,
    //string CellphoneNumber,

    string RoleName,
    string RoleNameEnglish,

    bool IsSuspended,

    string SuspensionReason,

    DateTime EntranceDate,

    string EntranceDateFormatMonthShort,
    string EntranceDateFormatMonthShortEnglish
);
