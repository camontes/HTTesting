namespace HR_Platform.Application.Assignations.Common;

public record AssignationsResponse(
    Guid Id,

    string CompanyName,

    int NumberCollaborators,

    string AssignationName,

    string AssignationNameEnglish,

    bool IsEditable,

    bool IsDeleteable,

    bool IsInternalAssignation
);
