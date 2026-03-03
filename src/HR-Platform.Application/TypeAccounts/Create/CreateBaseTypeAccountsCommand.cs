using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TypeAccountEntities.Create;

public record CreateBaseTypeAccountsCommand(List<BaseTypeAccountEntityCommand> TypeAccountEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseTypeAccountEntityCommand(
    string Name,
    string NameEnglish
);

