using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

public record BaseUpdateContractTypeCommand
(
    Guid Id,
    string Name

) : IRequest<ErrorOr<bool>>;
