using HR_Platform.Application.BankAccounts.Common;
using HR_Platform.Application.Collaborators.UpdateEducationData;
using HR_Platform.Application.Collaborators.UpdateFamilyInformation;

namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorsByIdResponse(
    Guid Id,

    Guid CompanyId,
    Guid RoleId,
    string PensionId,
    string SeveranceBenefitId,
    Guid EducationalLevelId,
    string ProfessionalAdviceId,
    string HealthEntityId,
    Guid CollaboratorContractId,
    Guid TypeAccountId,

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

    string ProfessionalCard,

    string Birthdate,
    string Country,
    string Department,
    string City,
    string EconomicLevelId,
    string LocationAddress,
    string PhoneNumber,
    string PostalCode,

    BankAccountsResponse BankAccount,

    string Name,
    string Initials,

    //string PhoneNumber,
    //string CellphoneNumber,

    List<CollaboratorEducationResponse>? Educations,
    List<CollaboratorLifePreferenceResponse>? LifePreferences,
    List<LanguagesCollaboratorResponse>? Languages,
    List<TecnhologiesllaboratorResponse>? Tecnhologies,
    List<CollaboratorSoftSkillResponse>? SoftSkill,
    List<UpdateEmergencyContactCommand>? EmergencyContacts,

    int MaritalStatusId,
    int FamilyMembersNumber,
    int ChildrenNumber,
    List<UpdateFamilyCompositionCommand>? FamilyComposition,
    List<UpdateChildrenCommand>? Children,

    string RoleName,
    string RoleNameEnglish,

    string PhotoURL,
    string PhotoName,

    bool IsSuspended,

    string ChangedBy,
    string EmailChangedBy,
    string NameChangedBy,
    string TimeSinceChange,

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
