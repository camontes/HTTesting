using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.GetByCollaboratorId;

public record GetBaseWorkplaceRecommendationsByCollaboratorIdQuery(string? CollaboratorId, string Year) : IRequest<ErrorOr<WorkplaceRecommendationsResponse>>;