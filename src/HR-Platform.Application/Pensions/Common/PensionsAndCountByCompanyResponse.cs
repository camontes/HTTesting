namespace HR_Platform.Application.Pensions.Common;

public record PensionsAndCountByCompanyResponse
(
    List<PensionWIthCollaboratorCountResponse> Pensions,
    int PensionsCount
);
