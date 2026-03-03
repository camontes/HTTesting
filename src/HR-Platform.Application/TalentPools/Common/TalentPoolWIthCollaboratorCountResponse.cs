namespace HR_Platform.Application.TalentPools.Common;
public record TalentPoolWIthCollaboratorCountResponse
(
    Guid Id,

    string Tittle,
    string Description,
    string PostedTime,
    string PostedTimeEnglish,
    string PostedTimeToltip,
    string PostedTimeToltipEnglish,

    bool IsArchived,

    int NumberOfCollaborators,

    DateTime CreationDate,
    DateTime EditionDate
);
