using ErrorOr;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Application.TypeAccounts.Delete;

internal sealed class DeleteTypeAccountCommandHandler : IRequestHandler<DeleteTypeAccountCommand, ErrorOr<bool>>
{
    private readonly ITypeAccountRepository _typeAccountRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTypeAccountCommandHandler
    (
        ITypeAccountRepository typeAccountRepository,
        ICompanyRepository companyRepository,
        ICollaboratorRepository collaboratorRepository,
        IUnitOfWork unitOfWork
    )
    {
        _typeAccountRepository = typeAccountRepository ?? throw new ArgumentNullException(nameof(typeAccountRepository));
        _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(DeleteTypeAccountCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (query.TypeAccountList is not null && query.TypeAccountList.Count > 0)
        {

            //Default Type Account
            var NoneTypeAccount = await _typeAccountRepository.GetNoneTypeAccountByCompanyIdAsync(new CompanyId(query.CompanyId));

            if (NoneTypeAccount is null)
                return Error.NotFound("Default Type Id Not Found");

            List<TypeAccount> typeAccounts = await _typeAccountRepository
                .GetGroupByIds(query.TypeAccountList.Select(x => new TypeAccountId(x))
                .ToList());

            if (typeAccounts is null || typeAccounts.Count == 0)
                return Error.Validation("TypeAccounts", "Id List Not Matched With Provide Ids");

            List<TypeAccount> typeAccountsNoCollaborator = typeAccounts.Where(x => x.BankAccounts.Count == 0).ToList();
            
           _typeAccountRepository.DeleteRange(typeAccountsNoCollaborator);
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