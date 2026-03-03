namespace HR_Platform.Application.Assignations.Create;

public record BaseCreateAssignationElementCommand
(
    string Name,

    string NameEnglish,

    bool IsEditable,

    bool IsDeleteable,

    bool IsInternalAssignation
);
