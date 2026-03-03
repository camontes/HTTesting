using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.GetById;

public record GetOccupationalRecommendationByIdQuery(Guid OccupationalRecommendationId) : IRequest<ErrorOr<OccupationalRecommendationFileResponse>>;