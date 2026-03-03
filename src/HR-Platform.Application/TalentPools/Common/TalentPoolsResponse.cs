namespace HR_Platform.Application.TalentPools.Common;

public record TalentPoolsResponse(
    Guid Id,
    string Tittle,
    string Description,
    string PostedTime,
    DateTime CreationDate,
    DateTime EditionDate
);
