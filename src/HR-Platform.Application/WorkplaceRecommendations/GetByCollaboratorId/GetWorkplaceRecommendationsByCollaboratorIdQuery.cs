using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.GetByCollaboratorId;

public record GetWorkplaceRecommendationsByCollaboratorIdQuery(string? CollaboratorId, string Year, string EmailChangeBy) : IRequest<ErrorOr<WorkplaceRecommendationsResponse>>;