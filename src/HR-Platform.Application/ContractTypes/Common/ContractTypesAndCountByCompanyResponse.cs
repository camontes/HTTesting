namespace HR_Platform.Application.ContractTypes.Common;

public record ContractTypesAndCountByCompanyResponse
(
    List<ContractTypeWIthCollaboratorCountResponse> ContractTypes,
    int ContractTypesCount
);
