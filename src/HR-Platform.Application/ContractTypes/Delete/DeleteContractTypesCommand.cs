using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Delete;

public record DeleteContractTypesCommand
(
    List<Guid> ContractTypesList,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

