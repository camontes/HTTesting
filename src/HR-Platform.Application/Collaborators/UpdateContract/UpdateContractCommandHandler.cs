using ErrorOr;
using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.DefaultCurrencyTypes;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace Collaborators.UpdateContract;

internal sealed class UpdateContractsCommandHandler(
    ICollaboratorContractRepository collaboratorContractRepository,
    ICollaboratorRepository collaboratorRepository,
    IPositionRepository positionRepository,
    IAssignationTypeRepository assignationTypeRepository,
    IAssignationRepository assignationRepository,
    IContractTypeRepository contractTypeRepository,
    IDefaultCurrencyTypeRepository defaultCurrencyTypeRepository,
    IUnitOfWork unitOfWork
    )
    :
    IRequestHandler<UpdateContractsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorContractRepository _collaboratorContractRepository = collaboratorContractRepository ?? throw new ArgumentNullException(nameof(collaboratorContractRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IPositionRepository _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
    private readonly IAssignationTypeRepository _assignationTypeRepository = assignationTypeRepository ?? throw new ArgumentNullException(nameof(assignationTypeRepository));
    private readonly IAssignationRepository _assignationRepository = assignationRepository ?? throw new ArgumentNullException(nameof(assignationRepository));
    private readonly IContractTypeRepository _contractTypeRepository = contractTypeRepository ?? throw new ArgumentNullException(nameof(contractTypeRepository));
    private readonly IDefaultCurrencyTypeRepository _defaultCurrencyTypeRepository = defaultCurrencyTypeRepository ?? throw new ArgumentNullException(nameof(defaultCurrencyTypeRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateContractsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.Parse(command.CollaboratorId))) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (!await _positionRepository.ExistsAsync(new PositionId(Guid.TryParse(command.PositionId, out Guid resultPosition) ? resultPosition : Guid.NewGuid())))
            return Error.Validation("PositionId", "Position Id not Found");

        if (!await _assignationTypeRepository.ExistsAsync(new AssignationTypeId(command.AssignationTypeId)))
            return Error.Validation("AssignationTypeId", "Area not Found");

        if (!await _assignationRepository.ExistsAsync(new AssignationId(Guid.TryParse(command.AssignationId, out Guid resultAssignation) ? resultAssignation : Guid.NewGuid())))
            return Error.Validation("AssignationId", "Assignation Id not Found");

        if (!await _contractTypeRepository.ExistsAsync(new ContractTypeId(Guid.TryParse(command.ContractTypeId, out Guid resultContractType) ? resultContractType : Guid.NewGuid())))
            return Error.Validation("ContractTypeId", "ContractType Id not Found");

        if (string.IsNullOrEmpty(command.Salary) && command.CurrencyTypeId == 1)
            return Error.Validation("accountNumberString", "accountNumberString is not valid or missing");

        if (!await _defaultCurrencyTypeRepository.ExistsAsync(new DefaultCurrencyTypeId(command.CurrencyTypeId)))
            return Error.Validation("CurrencyTypeId", "Currency Type Id not Found");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Contracts.EditionDate", "EditionDate is not valid");


        if (!Guid.TryParse(command.Id, out Guid resultContractId))
            return Error.Validation("ContractId", "Bank Account Id is not valid or missing");

        CollaboratorContract? NoneContract = await _collaboratorContractRepository.GetNoneCollaboratorContractByCompanyIdAsync(new CompanyId(command.CompanyId));
        CollaboratorContractId ContractId = new(resultContractId);

        var rol = new PositionId(Guid.Parse(command.PositionId));
        var assignationType = new AssignationTypeId(command.AssignationTypeId);
        var assignation = new AssignationId(Guid.Parse(command.AssignationId));

        if (rol != oldCollaborator.PositionId)
        {
            oldCollaborator.PositionId = rol;
        }

        if (assignationType != oldCollaborator.AssignationTypeId)
        {
            oldCollaborator.AssignationTypeId = assignationType;
        }

        if (assignation != oldCollaborator.AssignationId)
        {
            oldCollaborator.AssignationId = assignation;
        }

        if (ContractId == NoneContract?.Id)
        {
            CollaboratorContract ContractRequest = new(
                new CollaboratorContractId(Guid.NewGuid()),
                new CompanyId(command.CompanyId),
                command.Salary,
                new ContractTypeId(Guid.Parse(command.ContractTypeId)),
                new DefaultCurrencyTypeId(command.CurrencyTypeId),
                !string.IsNullOrEmpty(command.Arl) ? command.Arl : string.Empty, //Arl
                !string.IsNullOrEmpty(command.Bonus) ? command.Bonus : string.Empty, //Bonus
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.BusinessEmail.ToString() : "superadminth@exsis.com.co", //EmailWhoChangedByTH
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : "Equipo de talento humano", //NameWhoChangedByTH
                true,
                true,
                editionDate,
                editionDate
                );

            _collaboratorContractRepository.Add(ContractRequest);
            oldCollaborator.CollaboratorContractId = ContractRequest.Id;
        }
        else
        {
            CollaboratorContract? oldContract = await _collaboratorContractRepository.GetByIdAsync(ContractId);
            var contractType = new ContractTypeId(Guid.Parse(command.ContractTypeId));
            var currencyType = new DefaultCurrencyTypeId(command.CurrencyTypeId);

            if (oldContract != null)
            {


                oldContract.ContractTypeId = contractType;
                oldContract.Salary = command.Salary;
                oldContract.DefaultCurrencyTypeId = currencyType;

                if (command.Arl is not null && command.Arl != oldContract.Arl)
                {
                    oldContract.Arl = command.Arl;
                }

                if (command.Bonus is not null && command.Bonus != oldContract.Bonus)
                {
                    oldContract.Bonus = command.Bonus;
                }

                oldContract.NameWhoChangedByTH = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : "Equipo de talento humano";
                oldContract.EmailWhoChangedByTH = command.EmailChangeBy;
                oldContract.EditionDate = editionDate;
                _collaboratorContractRepository.Update(oldContract);

            }
        }

        try
        {
            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;

            _collaboratorRepository.Update(oldCollaborator);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}