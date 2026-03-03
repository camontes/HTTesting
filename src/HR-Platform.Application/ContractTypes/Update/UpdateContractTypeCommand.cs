using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Update;

public record UpdateContractTypeCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;
