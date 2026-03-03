using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BankEntities.Create;

public record CreateBaseBanksCommand(List<BaseBankEntityCommand> BankEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseBankEntityCommand(
    string Name,
    string NameEnglish
);

