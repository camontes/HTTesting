namespace HR_Platform.Application.EvaluatorMembers.Common;

public record CollaboratorEvaluatorMemberListResponse(
    Guid Id,
    string Name,
    string Email,
    string BusinessEmail,
    string ShortName,
    string Photo
);
