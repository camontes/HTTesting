using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Create;

public record CreateTypeAccountsCommand(List<TypeAccountData> TypeAccountsDataList) : IRequest<ErrorOr<bool>>;

public record TypeAccountData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

