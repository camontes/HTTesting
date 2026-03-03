using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

internal sealed class CreateContractTypesCommandHandler(IContractTypeRepository ContractTypeRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateContractTypesCommand, ErrorOr<bool>>
{
    private readonly IContractTypeRepository _ContractTypeRepository = ContractTypeRepository ?? throw new ArgumentNullException(nameof(ContractTypeRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateContractTypesCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("ContractTypes.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("ContractTypes.EditionDate", "EditionDate is not valid");


        List<ContractType> ContractTypesToAdd = [];

        foreach (ContractTypeData ContractTypeData in command.ContractTypesDataList)
        {
            ContractType ContractType = new(
                new ContractTypeId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(ContractTypeData.CompanyId)),
                ContractTypeData.Name,
                ContractTypeData.NameEnglish,
                ContractTypeData.IsEditable,
                ContractTypeData.IsDeleteable,
                creationDate,
                editionDate
            );
            ContractTypesToAdd.Add(ContractType);
        }

        try
        {
            _ContractTypeRepository.AddRangeContractTypes(ContractTypesToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}