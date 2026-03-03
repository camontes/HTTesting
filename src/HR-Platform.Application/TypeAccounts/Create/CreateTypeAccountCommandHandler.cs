using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TypeAccounts;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Create;

internal sealed class CreateTypeAccountsCommandHandler(ITypeAccountRepository TypeAccountRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateTypeAccountsCommand, ErrorOr<bool>>
{
    private readonly ITypeAccountRepository _TypeAccountRepository = TypeAccountRepository ?? throw new ArgumentNullException(nameof(TypeAccountRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateTypeAccountsCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TypeAccounts.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("TypeAccounts.EditionDate", "EditionDate is not valid");


        List<TypeAccount> typeAccountsToAdd = [];

        foreach (TypeAccountData typeAccountData in command.TypeAccountsDataList)
        {
            TypeAccount typeAccount = new(
                new TypeAccountId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(typeAccountData.CompanyId)),
                typeAccountData.Name,
                typeAccountData.NameEnglish,
                typeAccountData.IsEditable,
                typeAccountData.IsDeleteable,
                creationDate,
                editionDate
            );
            typeAccountsToAdd.Add(typeAccount);
        }

        try
        {
            _TypeAccountRepository.AddRangeTypeAccounts(typeAccountsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}