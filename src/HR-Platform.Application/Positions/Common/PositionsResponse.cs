namespace HR_Platform.Application.Positions.Common;

public record PositionsResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    string Description,
    string DescriptionEnglish,

    string PositionFile,
    string PositionFileName,

    bool IsEditable,
    bool IsDeleteable,

    int CollaboratorsCount,

    DateTime CreationDate,
    DateTime EditionDate
);
