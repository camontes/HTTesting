using ErrorOr;
using HR_Platform.Application.BankAccounts.Common;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.UpdateEducationData;
using HR_Platform.Application.Collaborators.UpdateFamilyInformation;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.EmergencyContacts;
using HR_Platform.Domain.FamilyCompositions;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.SeveranceBenefits;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.GetById;

internal sealed class GetCollaboratorByIdQueryHandler(
    IBankAccountRepository BankAccountRepository,
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorEducationRepository collaboratorEducationRepository,
    ICollaboratorLifePreferenceRepository collaboratorLifePreferenceRepository,
    ICollaboratorLanguageRepository collaboratorLanguageRepository,
    ICollaboratorTechnologyToolRepository collaboratorTechnologyToolRepository,
    ICollaboratorSoftSkillRepository collaboratorSoftSkillRepository,
    IDocumentTypeRepository documentTypeRepository,
    IEncryptService encryptService,
    IFamilyCompositionRepository familyCompositionRepository,
    IPensionRepository pensionRepository,
    ISeveranceBenefitRepository severanceBenefitRepository,
    IHealthEntityRepository healthEntityRepository,
    IProfessionalAdviceRepository professionalAdviceRepository,
    IEmergencyContactRepository emergencyContactRepository,
    IChildrenRepository childrenRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetCollaboratorByIdQuery, ErrorOr<CollaboratorsByIdResponse>>
{
    private readonly IBankAccountRepository _bankAccountRepository = BankAccountRepository ?? throw new ArgumentNullException(nameof(BankAccountRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorEducationRepository _collaboratorEducationRepository = collaboratorEducationRepository ?? throw new ArgumentNullException(nameof(collaboratorEducationRepository));
    private readonly ICollaboratorLifePreferenceRepository _collaboratorLifePreferenceRepository = collaboratorLifePreferenceRepository ?? throw new ArgumentNullException(nameof(collaboratorLifePreferenceRepository));
    private readonly ICollaboratorLanguageRepository _collaboratorLanguageRepository = collaboratorLanguageRepository ?? throw new ArgumentNullException(nameof(collaboratorLanguageRepository));
    private readonly ICollaboratorTechnologyToolRepository _collaboratorTechnologyToolRepository = collaboratorTechnologyToolRepository ?? throw new ArgumentNullException(nameof(collaboratorTechnologyToolRepository));
    private readonly ICollaboratorSoftSkillRepository _collaboratorSoftSkillRepository = collaboratorSoftSkillRepository ?? throw new ArgumentNullException(nameof(collaboratorSoftSkillRepository));
    private readonly IChildrenRepository _childrenRepository = childrenRepository ?? throw new ArgumentNullException(nameof(childrenRepository));
    private readonly IDocumentTypeRepository _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));
    private readonly IEncryptService _encryptService = encryptService ?? throw new ArgumentNullException(nameof(encryptService));
    private readonly IFamilyCompositionRepository _familyCompositionRepository = familyCompositionRepository ?? throw new ArgumentNullException(nameof(familyCompositionRepository));
    private readonly IPensionRepository _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));
    private readonly IProfessionalAdviceRepository _professionalAdviceRepository = professionalAdviceRepository ?? throw new ArgumentNullException(nameof(professionalAdviceRepository));
    private readonly IEmergencyContactRepository _emergencyContactRepository = emergencyContactRepository ?? throw new ArgumentNullException(nameof(emergencyContactRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<CollaboratorsByIdResponse>> Handle(GetCollaboratorByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(query.Id) is not Collaborator collaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        DocumentType? documentTypeName = await _documentTypeRepository.GetByIdAsync(new DocumentTypeId(collaborator.DocumentTypeId.Value));

        Pension? pension = await _pensionRepository.GetNonePensionByCompanyIdAsync(new CompanyId(collaborator.CompanyId.Value));
        SeveranceBenefit? severanceBenefit = await _severanceBenefitRepository.GetNoneSeveranceBenefitByCompanyIdAsync(new CompanyId(collaborator.CompanyId.Value));
        HealthEntity? healthEntity = await _healthEntityRepository.GetNoneHealthEntityByCompanyIdAsync(new CompanyId(collaborator.CompanyId.Value));
        ProfessionalAdvice? professionalAdvice = await _professionalAdviceRepository.GetNoneProfessionalAdviceByCompanyIdAsync(new CompanyId(collaborator.CompanyId.Value));

        CollaboratorId collaboratorId = new(collaborator.Id.Value);

        Collaborator? collaboratorChanged = await _collaboratorRepository.GetByEmailAsync(collaborator.EmailChangedBy);
         
        string pensionTypeName = pension?.Id.Value.ToString() == collaborator.PensionId.Value.ToString() ? "Ninguno" : collaborator.PensionId.Value.ToString();
        string severanceBenefitTypeName = severanceBenefit?.Id.Value.ToString() == collaborator.SeveranceBenefitId.Value.ToString() ? "Ninguno" : collaborator.SeveranceBenefitId.Value.ToString();
        string healthEntityTypeName = healthEntity?.Id.Value.ToString() == collaborator.HealthEntityId.Value.ToString() ? "Ninguno" : collaborator.HealthEntityId.Value.ToString();
        string professionalAdviceTypeName = professionalAdvice?.Id.Value.ToString() == collaborator.ProfessionalAdviceId.Value.ToString() ? "Ninguno" : collaborator.ProfessionalAdviceId.Value.ToString();
        string economicLevelTypeName = collaborator.EconomicLevelId.Value == 1 ? "Ninguno" : collaborator.EconomicLevelId.Value.ToString();

        List<EmergencyContact> emergencyContacts = await _emergencyContactRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<CollaboratorLanguage> collaboratorLanguage = await _collaboratorLanguageRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<CollaboratorTechnologyTool> collaboratorTechnologyTool = await _collaboratorTechnologyToolRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<CollaboratorSoftSkill> collaboratorSoftSkills = await _collaboratorSoftSkillRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<CollaboratorLifePreference> collaboratorLifePreferences = await _collaboratorLifePreferenceRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<CollaboratorEducation> collaboratorEducations = await _collaboratorEducationRepository.GetByCollaboratorIdAsync(collaboratorId);

        BankAccount? resultBankAccount = await _bankAccountRepository.GetByIdAsync(collaborator.BankAccountId);


        List<UpdateEmergencyContactCommand> emergencyContactCollaborator = emergencyContacts.Select(x =>
            new UpdateEmergencyContactCommand(
                x.Id.Value.ToString(),
                x.CollaboratorId.Value.ToString(),
                x.ContactName,
                x.PhoneNumber,
                x.Relationship,
                x.Address,
                x.IsPrimaryContact
            )).ToList();

        List<LanguagesCollaboratorResponse> languagesCollaborator = collaboratorLanguage.Select(x =>
            new LanguagesCollaboratorResponse(
                x.Id is not null ? x.Id.Value.ToString() : string.Empty,
                x.DefaultLanguageTypeId is not null ? x.DefaultLanguageTypeId.Value : 0,
                x.DefaultLanguageLevelId is not null ? x.DefaultLanguageLevelId.Value : 0,
                x.DefaultLanguageType is not null ? x.DefaultLanguageType.Name : string.Empty,
                x.DefaultLanguageType is not null ? x.DefaultLanguageType.NameEnglish : string.Empty,
                x.DefaultLanguageLevel is not null ? x.DefaultLanguageLevel.Name : string.Empty,
                x.DefaultLanguageLevel is not null ? x.DefaultLanguageLevel.NameEnglish : string.Empty,
                x.OtherLanguageName is not null ? x.OtherLanguageName : string.Empty
            )).ToList();

        List<TecnhologiesllaboratorResponse> technologyToolCollaborator = collaboratorTechnologyTool.Select(x =>
            new TecnhologiesllaboratorResponse(
                x.Id is not null ? x.Id.Value.ToString() : string.Empty,
                x.DefaultTechnologyNameId is not null ? x.DefaultTechnologyNameId.Value : 0,
                x.DefaultKnowledgeLevelId is not null ? x.DefaultKnowledgeLevelId.Value : 0,

                x.DefaultTechnologyName is not null ? x.DefaultTechnologyName.Name : string.Empty,
                x.DefaultTechnologyName is not null ? x.DefaultTechnologyName.NameEnglish : string.Empty,

                x.DefaultKnowledgeLevel is not null ? x.DefaultKnowledgeLevel.Name : string.Empty,
                x.DefaultKnowledgeLevel is not null ? x.DefaultKnowledgeLevel.NameEnglish : string.Empty,

                x.OtherTechnologyName is not null ? x.OtherTechnologyName : string.Empty,
                x.OtherKnowledgeLevelNameEnglish is not null ? x.OtherKnowledgeLevelNameEnglish : string.Empty,
                x.OtherKnowledgeLevelName is not null ? x.OtherKnowledgeLevelName : string.Empty,
                x.OtherKnowledgeLevelNameEnglish is not null ? x.OtherKnowledgeLevelNameEnglish : string.Empty
            )).ToList();

         List<CollaboratorSoftSkillResponse> collaboratorSoftSkillsList = collaboratorSoftSkills.Select(x =>
            new CollaboratorSoftSkillResponse(
                x.Id is not null ? x.Id.Value.ToString() : string.Empty,
                x.DefaultSoftSkillId is not null ? x.DefaultSoftSkillId.Value : 0,

                x.DefaultSoftSkill is not null ? x.DefaultSoftSkill.Name : string.Empty,
                x.DefaultSoftSkill is not null ? x.DefaultSoftSkill.NameEnglish : string.Empty,

                x.OtherLanguageName is not null ? x.OtherLanguageName : string.Empty,
                x.OtherLanguageNameEnglish is not null ? x.OtherLanguageNameEnglish : string.Empty
            )).ToList();

         List<CollaboratorLifePreferenceResponse> collaboratorLifePreferencesList = collaboratorLifePreferences.Select(x =>
            new CollaboratorLifePreferenceResponse(
                x.Id is not null ? x.Id.Value.ToString() : string.Empty,
                x.DefaultLifePreferenceId is not null ? x.DefaultLifePreferenceId.Value : 0,

                x.DefaultLifePreference is not null ? x.DefaultLifePreference.Name : string.Empty,
                x.DefaultLifePreference is not null ? x.DefaultLifePreference.NameEnglish : string.Empty,

                x.OtherLanguageName is not null ? x.OtherLanguageName : string.Empty,
                x.OtherLanguageNameEnglish is not null ? x.OtherLanguageNameEnglish : string.Empty
            )).ToList();


         List<CollaboratorEducationResponse> collaboratorEducationsList = collaboratorEducations.Select(ce =>
            new CollaboratorEducationResponse(
                ce.Id is not null ? ce.Id.Value.ToString() : string.Empty,

                !string.IsNullOrEmpty(ce.InstitutionName) ? ce.InstitutionName : string.Empty,

                ce.DefaultProfessionId is not null ? ce.DefaultProfessionId.Value : 0,
                ce.DefaultProfession is not null ? ce.DefaultProfession.Name : string.Empty,
                ce.DefaultProfession is not null ? ce.DefaultProfession.NameEnglish : string.Empty,
                !string.IsNullOrEmpty(ce.OtherProfessionName) ? ce.OtherProfessionName : string.Empty,

                ce.EducationalLevelId is not null ? ce.EducationalLevelId.Value.ToString() : string.Empty,
                ce.EducationalLevel is not null ? ce.EducationalLevel.Name : string.Empty,
                ce.EducationalLevel is not null ? ce.EducationalLevel.NameEnglish : string.Empty,

                ce.DefaultStudyTypeId is not null ? ce.DefaultStudyTypeId.Value : 0,
                ce.DefaultStudyType is not null ? ce.DefaultStudyType.Name : string.Empty,
                ce.DefaultStudyType is not null ? ce.DefaultStudyType.NameEnglish : string.Empty,

                ce.IsCertificated,

                ce.DefaultStudyAreaId is not null ? ce.DefaultStudyAreaId.Value : 0,
                ce.DefaultStudyArea is not null ? ce.DefaultStudyArea.Name : string.Empty,
                ce.DefaultStudyArea is not null ? ce.DefaultStudyArea.NameEnglish : string.Empty,
                ce.IsCompletedStudy,
               
                ce.StartEducationDate is not null ? _timeFormatService.GetDateFormatMonthShort(ce.StartEducationDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")) : null,
                ce.EndEducationDate is not null ? _timeFormatService.GetDateFormatMonthShort(ce.EndEducationDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")) : null,

                ce?.DefaultEducationStageId is not null ? ce.DefaultEducationStageId.Value : null,
                ce?.DefaultEducationStage is not null ? ce.DefaultEducationStage.Name : string.Empty,
                ce?.DefaultEducationStage is not null ? ce.DefaultEducationStage.NameEnglish : string.Empty,

                ce?.EducationFileName,
                ce?.EducationFileURL
            )).ToList();


        List<Children> children = await _childrenRepository.GetByCollaboratorIdAsync(collaboratorId);
        List<FamilyComposition> familyCompositions = await _familyCompositionRepository.GetByCollaboratorIdAsync(collaboratorId);

        List<UpdateFamilyCompositionCommand> familyCompositionCollaborator = familyCompositions.Select(x =>
            new UpdateFamilyCompositionCommand(
                x.Id.Value.ToString(),
                x.CollaboratorId.Value.ToString(),
                x.Name,
                x.NameEnglish
            )).ToList();


        List<UpdateChildrenCommand> childrenCollaborator = children.Select(x =>
            new UpdateChildrenCommand(
                x.Id.Value.ToString(),
                x.CollaboratorId.Value.ToString(),
                x.Name,
                x.Age
            )).ToList();

        string DateSinceEdited = CalculateTimeDifference(collaborator.EditionDate.Value);


        BankAccountsResponse responseBankAccount = new (
            resultBankAccount?.Id is not null ? resultBankAccount.Id.Value.ToString() : string.Empty,
            resultBankAccount?.BankId is not null? resultBankAccount.BankId.Value.ToString() : string.Empty,
            resultBankAccount?.TypeAccountId is not null ? resultBankAccount.TypeAccountId.Value.ToString(): string.Empty,
            resultBankAccount is not null && !string.IsNullOrEmpty(resultBankAccount.AccountNumber) ? _encryptService.DecryptString(resultBankAccount.AccountNumber) : string.Empty
        );


        return new CollaboratorsByIdResponse
        (
            collaborator.Id.Value,

            collaborator.CompanyId.Value,
            collaborator.RoleId.Value,
            pensionTypeName,
            severanceBenefitTypeName,
            collaborator.EducationalLevelId.Value,
            professionalAdviceTypeName,
            healthEntityTypeName,
            collaborator.CollaboratorContractId.Value,
            collaborator.TypeAccountId.Value,

            collaborator.DocumentTypeId.Value,
            collaborator.OtherDocumentType,
            collaborator.AssignationId.Value,
            collaborator.CollaboratorStateId.Value,

            collaborator.Document,


            documentTypeName is not null ? documentTypeName.Name : string.Empty,
            documentTypeName is not null ? documentTypeName.NameEnglish : string.Empty,

            collaborator.Assignation is not null ? collaborator.Assignation.Name : string.Empty,
            collaborator.Assignation is not null ? collaborator.Assignation.NameEnglish : string.Empty,

            collaborator.BusinessEmail is not null ? collaborator.BusinessEmail.Value : string.Empty,
            collaborator.PersonalEmail is not null ? collaborator.PersonalEmail.Value : string.Empty,

            collaborator.ProfessionalCard is not null ? collaborator.ProfessionalCard : string.Empty,

            collaborator.Birthdate is not null && collaborator.Birthdate.Value != collaborator.CreationDate.Value ? collaborator.Birthdate.Value.ToString() : string.Empty, // En creacion se le coloca la fecha de ingreso por default
            collaborator.Country is not null ? collaborator.Country : string.Empty,
            collaborator.Department is not null ? collaborator.Department : string.Empty,
            collaborator.City is not null ? collaborator.City : string.Empty,
            economicLevelTypeName,
            collaborator.LocationAddress is not null ? collaborator.LocationAddress : string.Empty,
            collaborator.PhoneNumber is not null ? collaborator.PhoneNumber : string.Empty,
            collaborator.PostalCode is not null ? collaborator.PostalCode : string.Empty,

            responseBankAccount,

            collaborator.Name, //Name
            _stringService.GetInitials(collaborator.Name), //Initial Name

            //collaborator.PhoneNumber is not null ? collaborator.PhoneNumber.Value : string.Empty,
            //collaborator.CellphoneNumber is not null ? collaborator.CellphoneNumber.Value : string.Empty,
            
            collaboratorEducationsList, // Education
            collaboratorLifePreferencesList, //Life Reference
            languagesCollaborator, //Languages
            technologyToolCollaborator, //Technology
            collaboratorSoftSkillsList, //Soft Skill
            emergencyContactCollaborator, //Emergency Contact

            collaborator.MaritalStatusId.Value, //MaritalStatusId
            collaborator.FamilyMembersNumber, //FamilyMembersNumber
            collaborator.ChildrenNumber, // ChildrenNumber
            familyCompositionCollaborator,//Family Conposition
            childrenCollaborator, //Children

            collaborator.Role is not null ? collaborator.Role.Name : string.Empty,
            collaborator.Role is not null ? collaborator.Role.NameEnglish : string.Empty,

            !string.IsNullOrEmpty(collaborator.Photo) ? collaborator.Photo : string.Empty, // PhotoURL
            !string.IsNullOrEmpty(collaborator.PhotoName) ? collaborator.PhotoName : string.Empty, //PhotoName

            collaborator.IsSuspended, //IsSupended

            collaborator.ChangedBy,
            collaborator.EmailChangedBy,
            collaboratorChanged is not null ? collaboratorChanged.Name : string.Empty,
            !string.IsNullOrEmpty(DateSinceEdited) ? DateSinceEdited : string.Empty,

            collaborator.SuspensionReason,

            collaborator.EntranceDate.Value,
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMMM/dd/yyyy", new CultureInfo("en-US")),

            collaborator.EditionDate.Value,
            _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
        );
    }
    public static string CalculateTimeDifference(DateTime fromDate)
    {
        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

        TimeSpan difference = horaColombiana - fromDate;

        if (difference.TotalMinutes < 60)
        {
            return $"{(int)difference.TotalMinutes} minutos";
        }
        else if (difference.TotalHours < 24)
        {
            return $"{(int)difference.TotalHours} horas";
        }
        else if (difference.TotalDays < 30)
        {
            return $"{(int)difference.TotalDays} días";
        }
        else if (difference.TotalDays < 365)
        {
            int months = (int)(difference.TotalDays / 30);
            return $"{months} {(months == 1 ? "mes" : "meses")}";
        }
        else
        {
            int years = (int)(difference.TotalDays / 365);
            return $"{years} {(years == 1 ? "año" : "años")}";
        }
    }
}