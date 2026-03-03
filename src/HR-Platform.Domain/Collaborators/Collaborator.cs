using HR_Platform.Domain.Acts;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.CollaboratorStates;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.DocumentManagements;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.EmergencyContacts;
using HR_Platform.Domain.FamilyCompositions;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.NoteViewers;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.OccupationalTests;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.WorkplaceEvidences;
using HR_Platform.Domain.WorkplaceInformations;
using HR_Platform.Domain.WorkplaceRecommendations;

namespace HR_Platform.Domain.Collaborators;

public sealed class Collaborator : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private Collaborator()
    {
    }

#pragma warning disable CS8601 // Posible asignación de referencia nula


    public Collaborator
    (
        AssignationTypeId assignationTypeId,
        AssignationId assignationId,
        BankId bankId,
        BankAccountId bankAccountId,
        CollaboratorId id,
        CollaboratorContractId collaboratorContractId,
        CollaboratorStateId collaboratorStateId,
        CompanyId companyId,
        DocumentTypeId documentTypeId,
        string otherDocumentType,
        EducationalLevelId educationalLevelId,
        HealthEntityId healthEntityId,
        MaritalStatusId maritalStatusId,
        PensionId pensionId,
        PositionId positionId,
        ProfessionalAdviceId professionalAdviceId,
        RoleId roleId,
        SeveranceBenefitId severanceBenefitId,
        TypeAccountId typeAccountId,
        Email businessEmail,
        Email? personalEmail,

        string name,
        string document,

        TimeDate birthdate,
        string country,
        string department,
        string city,
        string locationAddress,
        EconomicLevelId economicLevelId,
        string phoneNumber,
        string postalCode,

        Address address,

        string cvFile,
        string photo,
        string photoName,

        string professionalCard,

        int familyMembersNumber,
        int childrenNumber,

        string suspensionReason,

        bool sendNotificationsToPersonalEmail,
        bool isPendingInvitation,
        bool alreadyLogin,
        bool isSuspended,
        bool showNewFeatures,

        bool isCopasstMember,
        bool isCoexistenceCommitteeMember,
        bool isEvaluator,

        string changedBy,
        string emailChangedBy,

        string loginCode,
        string recoveryCode,

        TimeDate entranceDate,
        TimeDate creationDate,
        TimeDate editionDate

        )
    {
        Id = id;

        AssignationTypeId = assignationTypeId;
        AssignationId = assignationId;
        BankId = bankId;
        BankAccountId = bankAccountId;
        CollaboratorContractId = collaboratorContractId;
        CollaboratorStateId = collaboratorStateId;
        CompanyId = companyId;

        DocumentTypeId = documentTypeId;
        OtherDocumentType = otherDocumentType;

        EducationalLevelId = educationalLevelId;
        HealthEntityId = healthEntityId;
        MaritalStatusId = maritalStatusId;

        PensionId = pensionId;
        PositionId = positionId;
        ProfessionalAdviceId = professionalAdviceId;
        RoleId = roleId;
        SeveranceBenefitId = severanceBenefitId;
        TypeAccountId = typeAccountId;

        BusinessEmail = businessEmail;
        PersonalEmail = personalEmail;

        Name = name;

        Document = document;
        Address = address;

        EntranceDate = entranceDate;
        CreationDate = creationDate;
        EditionDate = editionDate;


        Birthdate = birthdate;
        Country = country;
        Department = department;
        City = city;
        LocationAddress = locationAddress;
        EconomicLevelId = economicLevelId;
        PhoneNumber = phoneNumber;
        PostalCode = postalCode;

        CVFile = cvFile;
        Photo = photo;
        PhotoName = photoName;

        ProfessionalCard = professionalCard;

        FamilyMembersNumber = familyMembersNumber;
        ChildrenNumber = childrenNumber;

        SuspensionReason = suspensionReason;

        SendNotificationsToPersonalEmail = sendNotificationsToPersonalEmail;
        IsPendingInvitation = isPendingInvitation;
        AlreadyLogin = alreadyLogin;
        IsSuspended = isSuspended;
        ShowNewFeatures = showNewFeatures;

        IsCopasstMember = isCopasstMember;
        IsCoexistenceCommitteeMember = isCoexistenceCommitteeMember;
        IsEvaluator = isEvaluator;

        ChangedBy = changedBy;
        EmailChangedBy = emailChangedBy;

        LoginCode = loginCode;
        RecoveryCode = recoveryCode;
    }

#pragma warning restore CS8601 // Posible asignación de referencia nula

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorId Id { get; set; }

    public AssignationTypeId AssignationTypeId { get; set; }
    public AssignationType AssignationType { get; set; }
    public AssignationId AssignationId { get; set; }
    public Assignation Assignation { get; set; }

    public BankId BankId { get; set; }
    public Bank Bank { get; set; }

    public BankAccountId BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }

    public CollaboratorStateId CollaboratorStateId { get; set; }
    public CollaboratorState CollaboratorState { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public CollaboratorContractId CollaboratorContractId { get; set; }
    public CollaboratorContract CollaboratorContract { get; set; }

    public DocumentTypeId DocumentTypeId { get; set; }
    public DocumentType? DocumentType { get; set; }
    public string OtherDocumentType { get; set; }

    public EducationalLevelId EducationalLevelId { get; set; }
    public EducationalLevel EducationalLevel { get; set; }

    public HealthEntityId HealthEntityId { get; set; }
    public HealthEntity HealthEntity { get; set; }

    public MaritalStatusId MaritalStatusId { get; set; }
    public MaritalStatus MaritalStatus { get; set; }

    public PensionId PensionId { get; set; }
    public Pension Pension { get; set; }

    public ProfessionalAdviceId ProfessionalAdviceId { get; set; }
    public ProfessionalAdvice ProfessionalAdvice { get; set; }

    public PositionId PositionId { get; set; }
    public Position Position { get; set; }

    public SeveranceBenefitId SeveranceBenefitId { get; set; }
    public SeveranceBenefit SeveranceBenefit { get; set; }

    public TypeAccountId TypeAccountId { get; set; }
    public TypeAccount TypeAccount { get; set; }

    public RoleId RoleId { get; set; }
    public Role Role { get; set; }

    public Email BusinessEmail { get; set; }
    public Email PersonalEmail { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;
    public Address Address { get; set; }

    //Location Data
    public TimeDate Birthdate { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public EconomicLevelId EconomicLevelId { get; set; }
    public EconomicLevel EconomicLevel { get; set; }
    public string LocationAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;

    public string CVFile { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string PhotoName { get; set; } = string.Empty; // New Field > 09/24
    public string ProfessionalCard { get; set; } = string.Empty;

    public bool SendNotificationsToPersonalEmail { get; set; }

    public string LoginCode { get; set; } = string.Empty;
    public string RecoveryCode { get; set; } = string.Empty;

    public string SuspensionReason { get; set; } = string.Empty;

    public int FamilyMembersNumber { get; set; }
    public int ChildrenNumber { get; set; }

    public bool IsPendingInvitation { get; set; }
    public bool AlreadyLogin { get; set; }
    public bool IsSuspended { get; set; }
    public bool ShowNewFeatures { get; set; }

    public bool IsCopasstMember { get; set; }
    public bool IsCoexistenceCommitteeMember { get; set; }
    public bool IsEvaluator { get; set; }

    public string ChangedBy { get; set; }
    public string EmailChangedBy { get; set; }


    //public TimeDate BirthDate { get; set; }
    public TimeDate EntranceDate { get; set; }
    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<Act> Acts { get; set; }
    public List<BenefitClaimAnswer> BenefitClaimAnswers { get; set; }
    public List<BrigadeMember> BrigadeMembers { get; set; }
    public List<Children> Children { get; set; }
    public List<CollaboratorBenefitClaim> CollaboratorBenefitClaims { get; set; }
    public List<CollaboratorBrigade> CollaboratorBrigades { get; set; }
    public List<CollaboratorCriteria> CollaboratorCriterias { get; set; }
    public List<CollaboratorCriteria> Evaluators { get; set; }
    public List<CollaboratorEducation> CollaboratorEducations { get; set; }
    public List<CollaboratorEvent> CollaboratorEvents { get; set; }
    public List<CollaboratorGeneralInduction> CollaboratorGeneralInductions { get; set; }
    public List<CollaboratorInduction> CollaboratorInductions { get; set; }
    public List<CollaboratorLanguage> CollaboratorLanguages { get; set; }
    public List<CollaboratorLifePreference> CollaboratorLifePreferences { get; set; }
    public List<CollaboratorSoftSkill> CollaboratorSoftSkills { get; set; }
    public List<CollaboratorTag> CollaboratorTags { get; set; }
    public List<CollaboratorTalentPool> CollaboratorTalentPools { get; set; }
    public List<CollaboratorTechnologyTool> CollaboratorTechnologyTools { get; set; }
    public List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswers { get; set; }
    public List<DocumentManagement> DocumentManagements { get; set; }
    public List<EmergencyContact> EmergencyContacts { get; set; }
    public List<FamilyComposition> FamilyCompositions { get; set; }
    public List<FormAnswerGroup> FormAnswerGroups { get; set; }
    public List<FormAnswer> FormAnswers { get; set; }
    public List<Notification> Notifications { get; set; }
    public List<NotificationNote> NotificationNotes { get; set; }
    public List<Note> CreatedNotes { get; set; }
    public List<Note> AssigneeNotes { get; set; }
    public List<NoteViewer> NoteViewers { get; set; }
    public List<OccupationalTest> OccupationalTests { get; set; }
    public List<OccupationalRecommendation> OccupationalRecommendations { get; set; }
    public List<WorkplaceInformation> WorkplaceInformations { get; set; }
    public List<WorkplaceEvidence> WorkplaceEvidences { get; set; }
    public List<WorkplaceRecommendation> WorkplaceRecommendations { get; set; }
}

