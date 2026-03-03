using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateContractTypesCommand(List<ContractTypeData> ContractTypesDataList) : IRequest<ErrorOr<bool>>;

public record ContractTypeData(
    string CompanyId,
    string Name,
    string NameEnglish,
    bool IsEditable,
    bool IsDeleteable
);

