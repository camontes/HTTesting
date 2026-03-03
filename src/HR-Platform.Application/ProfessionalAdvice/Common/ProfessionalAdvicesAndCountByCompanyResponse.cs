namespace HR_Platform.Application.ProfessionalAdvices.Common;

public record ProfessionalAdvicesAndCountByCompanyResponse
(
    List<ProfessionalAdviceWIthCollaboratorCountResponse> ProfessionalAdvices,
    int ProfessionalAdvicesCount
);
