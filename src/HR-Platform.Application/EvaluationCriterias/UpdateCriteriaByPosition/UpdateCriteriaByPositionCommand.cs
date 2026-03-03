using ErrorOr;
using MediatR;
namespace HR_Platform.Application.EvaluationCriterias.UpdateCriteriaByPosition; 
 
public record UpdateCriteriaByPositionCommand(
    Guid PositionId,
    List<BaseCriterias> CriteriasList,
    int CriteriaIdentifier
    ) : IRequest<ErrorOr<bool>>;

public record BaseCriterias(
    Guid Id,
    string Name,
    string Description,
    int Percentage
);