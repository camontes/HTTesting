namespace HR_Platform.Application.Collaborators.Common;

public record TecnhologiesllaboratorResponse
(
    string TechnologyId,
    int? TechnologyNameId,
    int? KnowledgeLevelId,

    string? TechnologyName,
    string? TechnologyNameEnglish,
    string? KnowledgeLevel,
    string? KnowledgeLevelEnglish,

    string? OtherTechnologyName,
    string? OtherTechnologyNameEnglish,

    string? OtherKnowledgeLevelName,
    string? OtherKnowledgeLevelNameEnglish
);