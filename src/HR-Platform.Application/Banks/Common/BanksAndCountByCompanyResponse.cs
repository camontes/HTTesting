namespace HR_Platform.Application.Banks.Common;

public record BanksAndCountByCompanyResponse
(
    List<BankWIthCollaboratorCountResponse> Banks,
    int BanksCount
);
