using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.GetById;

public record GetWorkplaceRecommendationByIdQuery(Guid WorkplaceRecommendationId) : IRequest<ErrorOr<WorkplaceRecommendationFileResponse>>;