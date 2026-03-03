namespace HR_Platform.Application.Assignations.Create;

public record CreateAssignationElementCommand
(
    string CompanyId,

    string Name,

    string NameEnglish,

    bool IsEditable,

    bool IsDeleteable,

    bool IsInternalAssignation
);
