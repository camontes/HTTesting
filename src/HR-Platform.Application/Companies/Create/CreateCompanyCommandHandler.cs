using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DefaultAssignations;
using HR_Platform.Domain.DefaultRoles;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.DocumentTypes;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.CollaboratorStates;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.Banks;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.MaritalStatuses;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.BankAccounts;

namespace HR_Platform.Application.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    IAssignationRepository assignationRepository,
    IBankAccountRepository bankAccountRepository,
    IBankRepository bankRepository,
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorContractRepository CollaboratorContractRepository,
    ICompanyRepository companyRepository,
    IDefaultAssignationRepository defaultAssignationRepository,
    IDefaultRoleRepository defaultRoleRepository,
    IEducationalLevelRepository educationalLevelRepository,
    IHealthEntityRepository healthEntityRepository,
    IPensionRepository pensionRepository,
    IPositionRepository positionRepository,
    IProfessionalAdviceRepository professionalAdviceRepository,
    IRoleRepository roleRepository,
    ISeveranceBenefitRepository severanceBenefitRepository,
    ITypeAccountRepository typeAccountRepository,


    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCompanyCommand, ErrorOr<Guid>>
{
    private readonly IAssignationRepository _assignationRepository = assignationRepository ?? throw new ArgumentNullException(nameof(assignationRepository));
    private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
    private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IDefaultAssignationRepository _defaultAssignationRepository = defaultAssignationRepository ?? throw new ArgumentNullException(nameof(defaultAssignationRepository));
    private readonly IDefaultRoleRepository _defaultRoleRepository = defaultRoleRepository ?? throw new ArgumentNullException(nameof(defaultRoleRepository));
    private readonly IEducationalLevelRepository _educationalLevelRepository = educationalLevelRepository ?? throw new ArgumentNullException(nameof(educationalLevelRepository));
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));
    private readonly IPensionRepository _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
    private readonly ICollaboratorContractRepository _collaboratorContractRepository = CollaboratorContractRepository ?? throw new ArgumentNullException(nameof(CollaboratorContractRepository));
    private readonly IProfessionalAdviceRepository _professionalAdviceRepository = professionalAdviceRepository ?? throw new ArgumentNullException(nameof(professionalAdviceRepository));
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
    private readonly ITypeAccountRepository _typeAccountRepository = typeAccountRepository ?? throw new ArgumentNullException(nameof(typeAccountRepository));

    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Guid>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        /* Creation and edition dates */

        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        /* Guids */

        CollaboratorId superAdminId = new(Guid.NewGuid());
        CompanyId companyId = new(Guid.NewGuid());
        RoleId superAdminRoleId = new(Guid.NewGuid());

        /* Company validations */

        if (Email.Create(command.Email) is not Email email)
            return Error.Validation("Companies.Email", "Email has not valid format");

        if (Email.Create(command.RequestsEmail) is not Email requestsEmail)
            return Error.Validation("Companies.RequestsEmail", "RequestsEmail has not valid format");

        if (Address.Create(command.StreetAddress, command.CountryCode, command.Country,
            command.StateCode, command.State, command.CityCode, command.City, command.ZipCode) is not Address address)
            return Error.Validation("Companies.Address", "Address is not valid");

        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            return Error.Validation("Companies.PhoneNumber", "PhoneNumber has not valid format");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Companies.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Companies.EditionDate", "EditionDate is not valid");

        /* Company mapping */

        Company company = new
        (
            companyId,

            email,
            requestsEmail,

            command.CompanyName,
            !string.IsNullOrEmpty(command.MenuName) ? command.MenuName : string.Empty,

            address,

            phoneNumber,

            command.LogoURL,
            command.LogoName,
            
            !string.IsNullOrEmpty(command.URL) ? command.URL : string.Empty,

            creationDate,
            editionDate
        );

        /* Create default roles */

        List<DefaultRole> defaultRoles = await _defaultRoleRepository.GetAll();

        if (defaultRoles is not null && defaultRoles.Count > 0)
        {
            foreach (DefaultRole defaultRole in defaultRoles)
            {
                /* Guid */

                RoleId roleId = new(Guid.NewGuid());

                /* Role mapping */

                Role role = new
                (
                    roleId,
                    companyId,
                    defaultRole.Name,
                    defaultRole.NameEnglish,
                    new AreaId(Guid.NewGuid()), // Fix - FK not Works
                    false,
                    false,
                    creationDate,
                    editionDate
                );
                
                /* Add Role */

                _roleRepository.Add(role);

                /* Validate if the role is SuperAdmin */

                if (defaultRole.Id == new DefaultRoleId(1))
                    superAdminRoleId = roleId;
            }
        }

        /* Create default assignations */

        List<DefaultAssignation> defaultAssignations = await _defaultAssignationRepository.GetAll();

        if (defaultAssignations is not null && defaultAssignations.Count > 0)
        {
            foreach (DefaultAssignation defaultAssignation in defaultAssignations)
            {
                /* Guid */

                AssignationId assignationId = new(Guid.NewGuid());

               /* Validate if is internal assignation */

                bool isInternalAssignation = true;

                if (defaultAssignation.Id == new DefaultAssignationId(2))
                    isInternalAssignation = false;

                /* Assignation mapping */

                Assignation assignation = new
                (
                    assignationId,
                    companyId,
                    defaultAssignation.Name,
                    defaultAssignation.NameEnglish,
                    false,
                    false,
                    isInternalAssignation,
                    creationDate,
                    editionDate
                );

                /* Add Assignation */

                _assignationRepository.Add(assignation);
            }
        }

        /* Collaboators validations */

        if (TimeDate.Create(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) is not TimeDate entranceDate)
            return Error.Validation("Collaborators.EntranceDate", "EntranceDate is not valid");

        if (TimeDate.Create("01/01/1990") is not TimeDate birthDate)
            return Error.Validation("Collaborators.BirthDate", "BirthDate is not valid");

        /* Get Assignations */

        List<Assignation>? assignationsCreated = await _assignationRepository.GetByCompanyIdAndInternalOrExternalAsync(companyId, true);

        Guid createdAssignationId = new();

        if (assignationsCreated is not null && assignationsCreated.Count > 0)
            createdAssignationId = assignationsCreated[0].Id.Value;

        /* Default Pension */
        Pension? pension = await _pensionRepository.GetNonePensionByCompanyIdAsync(companyId);

        /* Default Bank */
        Bank? bank = await _bankRepository.GetNoneBankByCompanyIdAsync(companyId);

        /* Default Bank Account */
        BankAccount? bankAccount = await _bankAccountRepository.GetNoneBankAccountByAccountNumberAsync();

        /* Default Collaborator Contracts */
        CollaboratorContract? collaboratorContract = await _collaboratorContractRepository.GetNoneCollaboratorContractByCompanyIdAsync(companyId);

        /* Default EducationalLevel */
        EducationalLevel? educationalLevel = await _educationalLevelRepository.GetNoneEducationalLevelByCompanyIdAsync(companyId);

        /* Default Health Entity */
        HealthEntity? healthEntity = await _healthEntityRepository.GetNoneHealthEntityByCompanyIdAsync(companyId);

        /* Default ProfessionalAdvice */
        ProfessionalAdvice? professionalAdvice = await _professionalAdviceRepository.GetNoneProfessionalAdviceByCompanyIdAsync(companyId);

        /* Default SeveranceBenefit */
        SeveranceBenefit? severanceBenefit = await _severanceBenefitRepository.GetNoneSeveranceBenefitByCompanyIdAsync(companyId);

        /* Default Type Account */
        TypeAccount? typeAccount = await _typeAccountRepository.GetNoneTypeAccountByCompanyIdAsync(companyId);

        Role? SuperAdminRole = await _roleRepository.GetByCompanyIdSuperAdminAsync(companyId);


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

        Position? position = await _positionRepository.GetCollaboratorPositionByCompanyIdAsync(companyId);

        string positionId = string.Empty;

        if (position is not null)
            positionId = position.Id.Value.ToString();

        /* SuperAdmin mapping */

        Collaborator superAdmin = new
        (
            new AssignationTypeId(2), // Internal Staff
            new AssignationId(createdAssignationId),
            new BankId(Guid.Parse(bankId)), //None
            new BankAccountId(Guid.Parse(bankAccountId)), //None

            superAdminId,
            new CollaboratorContractId(Guid.Parse(collaboratorContractId)),// None
            new CollaboratorStateId(1), // Active
            companyId,

            new DocumentTypeId(8), // Other
            "",
            new EducationalLevelId(Guid.Parse(educationalLevelId)), // None
            new HealthEntityId(Guid.Parse(healthEntityId)), // None
            new MaritalStatusId(1), // Marital Status None

            new PensionId(Guid.Parse(pensionId)), // None
            new PositionId(Guid.Parse(positionId)),// Collaborator
            new ProfessionalAdviceId(Guid.Parse(professionalAdviceId)), // None
            superAdminRoleId,
            new SeveranceBenefitId(Guid.Parse(severanceBenefitId)), // None
            new TypeAccountId(Guid.Parse(typeAccountId)), //None

            email,
            null, // PersonalEmail

            command.Name,

            command.Document,

            //phoneNumber,
            //null, // CellPhoneNumber
            birthDate,
            string.Empty, //Country
            string.Empty, //Deparment
            string.Empty, //City
            string.Empty, //LocationAddress
            new EconomicLevelId(1),
            string.Empty, // PhoneNumber
            string.Empty, // PostalCode

            address,

            string.Empty, // CVFile
            string.Empty, // Photo
            string.Empty, // PhotoName

            command.ProfessionalCard, 

            0, // Family Members Number
            0, // Children Number

            string.Empty, // SuspensionReason

            false, // SendNotificationsToPersonalEmail
            false, // IsPendingInvitation
            false, // AlreadyLogin
            false, // IsSuspended
            true, // ShowNewFeatures

            false, // IsCopasstMember
            false, // IsCoexistenceCommitteeMember
            false, // IsEvaluator
            SuperAdminRole is not null ? SuperAdminRole.Name : "Superadministrador",  //ChangeBy
            "superadminth@exsis.com.co",//EmailChangeBy

            string.Empty, // LoginCode
            string.Empty, // RecoveryCode

            //birthDate,
            entranceDate,
            creationDate,
            editionDate
        );

        /* Add Company and SuperAdmin */

        _companyRepository.Add(company);
        _collaboratorRepository.Add(superAdmin);

        /* Save changes */

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        /* Return Company */

        return company.Id.Value;
    }
}