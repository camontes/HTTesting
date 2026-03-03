using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.GetYearsByCollaboratorId;

public record GetWorkplaceRecommendationYearsByCollaboratorIdQuery(Guid CollaboratorId) : IRequest<ErrorOr<WorkplaceRecommendationFileYearsListResponse>>;


