namespace HR_Platform.Application.EducationalLevels.Common;

public record EducationalLevelsAndCountByCompanyResponse
(
    List<EducationalLevelWIthCollaboratorCountResponse> EducationalLevels,
    int EducationalLevelsCount
);
