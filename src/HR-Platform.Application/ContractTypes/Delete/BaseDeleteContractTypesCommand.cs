using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Delete;

public record BaseDeleteContractTypesCommand(List<Guid> ContractTypesList) : IRequest<ErrorOr<bool>>;

