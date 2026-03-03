namespace HR_Platform.Application.TypeAccounts.Common;

public record TypeAccountsAndCountByCompanyResponse
(
    List<TypeAccountWIthCollaboratorCountResponse> TypeAccounts,
    int TypeAccountsCount
);
