using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByPositionId;

public record GetCollaboratorByPositionIdQuery(Guid PositonId) : IRequest<ErrorOr<List<CollaboratorListResponse>>>;