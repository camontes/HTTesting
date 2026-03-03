using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Banks.Create;

public record CreateBanksCommand(List<BankData> BanksDataList) : IRequest<ErrorOr<bool>>;

public record BankData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

