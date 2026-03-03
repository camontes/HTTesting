using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

internal sealed class UpdateContractTypeCommandHandler : IRequestHandler<UpdateContractTypeCommand, ErrorOr<bool>>
{
    private readonly IContractTypeRepository _contractTypeRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateContractTypeCommandHandler
    (
        IContractTypeRepository contractTypeRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _contractTypeRepository = contractTypeRepository ?? throw new ArgumentNullException(nameof(contractTypeRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateContractTypeCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _contractTypeRepository.GetByIdAsync(new ContractTypeId(query.Id)) is not ContractType oldContractType)
        {
            return Error.NotFound("ContractType.NotFound", "The contract Type with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (!string.IsNullOrEmpty(query.Name) && query.Name != oldContractType.Name)
        {
            oldContractType.Name = query.Name;
            oldContractType.EditionDate = editionDate;
            _contractTypeRepository.Update(oldContractType);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }
}