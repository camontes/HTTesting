namespace HR_Platform.Application.EvaluatorMembers.Common;
public record EvaluatorMemberResponse
(
    Guid Id,
    string Name,
    string Position,
    string PositionEnglish,
    string Photo,
    string ShortName, 
    bool HasCollaboratorsAssined,
    List<PositionNameResponse> PositionsByEvaluators
);

public record PositionNameResponse
(
    string PositionName,
    string PositionNameEnglish
);