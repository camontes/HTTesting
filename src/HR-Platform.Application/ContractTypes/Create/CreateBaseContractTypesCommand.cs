using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypeEntities.Create;

public record CreateBaseContractTypesCommand(List<BaseContractTypeEntityCommand> ContractTypeEntitiesList) : IRequest<ErrorOr<bool>>;

public record BaseContractTypeEntityCommand(
    string Name,
    string NameEnglish
);

