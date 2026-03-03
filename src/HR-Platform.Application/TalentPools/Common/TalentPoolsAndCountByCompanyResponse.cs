namespace HR_Platform.Application.TalentPools.Common;

public record TalentPoolsAndCountByCompanyResponse
(
    List<TalentPoolWIthCollaboratorCountResponse> TalentPoolsActive,
    List<TalentPoolWIthCollaboratorCountResponse> TalentPoolsArchive,
    int TalentPoolArchiveCount,
    int TalentPoolActiveCount
);
