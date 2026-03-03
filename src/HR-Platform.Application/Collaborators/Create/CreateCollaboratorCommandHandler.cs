using ErrorOr;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorStates;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.DocumentManagementFileTypes;
using HR_Platform.Domain.DocumentManagements;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Collaborators.Create;

internal sealed class CreateCollaboratorsCommandHandler(
    IBankRepository bankRepository,
    IBankAccountRepository bankAccountRepository,
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorContractRepository collaboratorContractRepository,
    IEducationalLevelRepository educationalLevelRepository,
    IHealthEntityRepository healthEntityRepository,
    IPensionRepository pensionRepository,
    IPositionRepository positionRepository,
    IProfessionalAdviceRepository professionalAdviceRepository,
    IRoleRepository roleRepository,
    IDocumentManagementRepository documentManagementRepository,
    ISeveranceBenefitRepository severanceBenefitRepository,
    ITypeAccountRepository typeAccountRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCollaboratorsCommand, ErrorOr<Guid>>
{
    private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
    private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorContractRepository _collaboratorContractRepository = collaboratorContractRepository ?? throw new ArgumentNullException(nameof(collaboratorContractRepository));
    private readonly IEducationalLevelRepository _educationalLevelRepository = educationalLevelRepository ?? throw new ArgumentNullException(nameof(educationalLevelRepository));
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));
    private readonly IPensionRepository _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly IProfessionalAdviceRepository _professionalAdviceRepository = professionalAdviceRepository ?? throw new ArgumentNullException(nameof(professionalAdviceRepository));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
    private readonly ITypeAccountRepository _typeAccountRepository = typeAccountRepository ?? throw new ArgumentNullException(nameof(typeAccountRepository));
    private readonly IDocumentManagementRepository _documentManagementRepository = documentManagementRepository ?? throw new ArgumentNullException(nameof(documentManagementRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Guid>> Handle(CreateCollaboratorsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        string editionDateString = creationDateString;

        string personalEmailValidation = !string.IsNullOrEmpty(command.PersonalEmail) ? command.PersonalEmail : command.BusinessEmail;

        Role? role = await _roleRepository.GetByCompanyIdAndRoleNameAsync(new CompanyId(Guid.Parse(command.CompanyId)), "Colaborador");

        string roleId = role != null && role.Id != null && !string.IsNullOrEmpty(role.Id.Value.ToString()) ? role.Id.Value.ToString() : string.Empty;

        if (Email.Create(command.BusinessEmail) is not Email businessEmail)
            return Error.Validation("Collaborators.BusinessEmail", "BusinessEmail has not valid format");

        if (Email.Create(personalEmailValidation) is not Email personalEmail)
            return Error.Validation("Collaborators.Personal Email", "PersonalEmail has not valid format");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);



        //if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        //    return Error.Validation("Collaborators.PhoneNumber", "PhoneNumber has not valid format");

        //if (PhoneNumber.Create(command.CellphoneNumber) is not PhoneNumber cellphoneNumber)
        //    return Error.Validation("Collaborators.CellphoneNumber", "CellphoneNumber has not valid format");

        //if (Address.Create(command.StreetAddress, command.CountryCode, command.Country,
        //    command.StateCode, command.State, command.CityCode, command.City, command.ZipCode) is not Address address)
        //    return Error.Validation("Collaborators.Address", "Address is not valid");

        if (Address.Create(command.StreetAddress, 0, string.Empty,
            0, string.Empty, 0, string.Empty, string.Empty) is not Address address)
            return Error.Validation("Collaborators.Address", "Address is not valid");

        //if (TimeDate.Create(command.BirthDate) is not TimeDate birthDate)
        //    return Error.Validation("Collaborators.BirthDate", "BirthDate is not valid");

        if (TimeDate.Create(command.EntranceDate) is not TimeDate entranceDate)
            return Error.Validation("Collaborators.EntranceDate", "EntranceDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Collaborators.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");


        CompanyId companyIdTemp = new(Guid.Parse(command.CompanyId));
        /* Default Pension */
        Pension? pension = await _pensionRepository.GetNonePensionByCompanyIdAsync(companyIdTemp);

        /* Default Bank */
        Bank? bank = await _bankRepository.GetNoneBankByCompanyIdAsync(companyIdTemp);

        /* Default Bank Account */
        BankAccount? bankAccount = await _bankAccountRepository.GetNoneBankAccountByAccountNumberAsync();

        /* Default Collaborator Contract */
        CollaboratorContract? collaboratorContract = await _collaboratorContractRepository.GetNoneCollaboratorContractByCompanyIdAsync(companyIdTemp);

        /* Default EducationalLevel */
        EducationalLevel? educationalLevel = await _educationalLevelRepository.GetNoneEducationalLevelByCompanyIdAsync(companyIdTemp);

        /* Default Health Entity */
        HealthEntity? healthEntity = await _healthEntityRepository.GetNoneHealthEntityByCompanyIdAsync(companyIdTemp);

        /* Default ProfessionalAdvice */
        ProfessionalAdvice? professionalAdvice = await _professionalAdviceRepository.GetNoneProfessionalAdviceByCompanyIdAsync(companyIdTemp);

        /* Default SeveranceBenefit */
        SeveranceBenefit? severanceBenefit = await _severanceBenefitRepository.GetNoneSeveranceBenefitByCompanyIdAsync(companyIdTemp);

        /* Default Type Account */
        TypeAccount? typeAccount = await _typeAccountRepository.GetNoneTypeAccountByCompanyIdAsync(companyIdTemp);

        Role? SuperAdminRole = await _roleRepository.GetByCompanyIdSuperAdminAsync(companyIdTemp);

        string bankId = string.Empty;
        string bankAccountId = string.Empty;
        string collaboratorContractId = string.Empty;
        string educationalLevelId = string.Empty;
        string healthEntityId = string.Empty;
        string pensionId = string.Empty;
        string professionalAdviceId = string.Empty;
        string severanceBenefitId = string.Empty;
        string typeAccountId = string.Empty;

        if (bank is not null)
            bankId = bank.Id.Value.ToString();

        if (bankAccount is not null)
            bankAccountId = bankAccount.Id.Value.ToString();

        if (collaboratorContract is not null)
            collaboratorContractId = collaboratorContract.Id.Value.ToString();

        if (educationalLevel is not null)
            educationalLevelId = educationalLevel.Id.Value.ToString();

        if (healthEntity is not null)
            healthEntityId = healthEntity.Id.Value.ToString();

        if (pension is not null)
            pensionId = pension.Id.Value.ToString();

        if (professionalAdvice is not null)
            professionalAdviceId = professionalAdvice.Id.Value.ToString();

        if (severanceBenefit is not null)
            severanceBenefitId = severanceBenefit.Id.Value.ToString();

        if (typeAccount is not null)
            typeAccountId = typeAccount.Id.Value.ToString();

        /* Default Position */

        Position? position = await _positionRepository.GetCollaboratorPositionByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));

        string positionId = string.Empty;

        if (position is not null)
            positionId = position.Id.Value.ToString();

        /* Create Collaborator */

        Collaborator collaborator = new
        (
            new AssignationTypeId(command.AssignationTypeId),
            new AssignationId(Guid.Parse(command.AssignationId)),
            new BankId(Guid.Parse(bankId)),
            new BankAccountId(Guid.Parse(bankAccountId)),

            new CollaboratorId(Guid.NewGuid()),
            new CollaboratorContractId(Guid.Parse(collaboratorContractId)),
            new CollaboratorStateId(1), // Active
            new CompanyId(Guid.Parse(command.CompanyId)),

            new DocumentTypeId(command.DocumentTypeId),
            command.OtherDocumentType,
            new EducationalLevelId(Guid.Parse(educationalLevelId)),
            new HealthEntityId(Guid.Parse(healthEntityId)), // None
            new MaritalStatusId(1), // Marital Status None

            new PensionId(Guid.Parse(pensionId)),
            new PositionId(Guid.Parse(positionId)),
            new ProfessionalAdviceId(Guid.Parse(professionalAdviceId)),
            new RoleId(Guid.Parse(roleId)),
            new SeveranceBenefitId(Guid.Parse(severanceBenefitId)),
            new TypeAccountId(Guid.Parse(typeAccountId)),

            businessEmail,
            personalEmail,//personalEmail,

            command.Name,
            command.Document,

            //phoneNumber,
            //cellphoneNumber,

            creationDate, // BirthDate,
            string.Empty, //Country
            string.Empty, //Deparment
            string.Empty, //City
            string.Empty, //LocationAddress
            new EconomicLevelId(1),
            string.Empty, // PhoneNumber
            string.Empty, // PostalCode

            address,

            command.CvFile,
            command.Photo,
            command.PhotoName,

            !string.IsNullOrEmpty(command.ProfessionalCard) ? command.ProfessionalCard : string.Empty, //Professional Card

            0, // Family Members Number
            0, // Children Number

            string.Empty, // SuspensionReason

            command.SendNotificationsToPersonalEmail,
            true, // IsPendingInvitation,
            false, // AlreadyLogin
            false, // IsSuspended
            true, // ShowNewFeatures

            false, //IsCopasstMember
            false, //IsCoexistenceCommitteeMember
            false, //IsEvaluator
            SuperAdminRole is not null ? SuperAdminRole.Name : "Superadministrador",  //ChangeBy
            "superadminth@exsis.com.co",//EmailChangeBy

            string.Empty, // LoginCode
            string.Empty, // RecoveryCode

            entranceDate,
            creationDate,
            editionDate
        );

        DocumentManagement documentManagement = new
           (
               new DocumentManagementId(Guid.NewGuid()),
               collaborator.Id,
               command.CvName,
               command.CvFile,
               command.EmailChangeBy,
               CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
               CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
               new DocumentManagementFileTypeId(2), // 2 -> HV
               "",
               true,
               true,
               creationDate,
               creationDate
        );

        _collaboratorRepository.Add(collaborator);

        _documentManagementRepository.Add(documentManagement);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return collaborator.Id.Value;
    }
}