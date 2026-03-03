using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ContractTypes;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Delete;

internal sealed class DeleteContractTypesCommandHandler(
    IContractTypeRepository contractTypeRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteContractTypesCommand, ErrorOr<bool>>
{
    private readonly IContractTypeRepository _contractTypeRepository = contractTypeRepository ?? throw new ArgumentNullException(nameof(contractTypeRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteContractTypesCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        /* Only 1 Severance Benefit validation */

        List<ContractType>? contractTypes = await _contractTypeRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        if (contractTypes == null || contractTypes.Count - query.ContractTypesList.Count < 1)
            return Error.NotFound("ContractType.Count", "The contract Types cannot be deleted, there must be at least one");

        /* Match ContractType */

        List<ContractType>? tcontractTypesMatched = contractTypes.Where(x => query.ContractTypesList.Any(y => new ContractTypeId(y) == x.Id && (x.CollaboratorContracts == null || x.CollaboratorContracts.Count == 0))).ToList();

        try
        {
            if (tcontractTypesMatched != null && tcontractTypesMatched.Count > 0)
            {
                _contractTypeRepository.DeleteRange(tcontractTypesMatched);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}