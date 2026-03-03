using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.GetYearsByCollaboratorId;

public record GetYearsByCollaboratorIdQuery(Guid CollaboratorId) : IRequest<ErrorOr<OccupationalRecommendationFileYearsListResponse>>;


