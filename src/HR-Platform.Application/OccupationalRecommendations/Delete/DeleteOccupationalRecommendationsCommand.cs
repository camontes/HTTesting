using ErrorOr;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.Delete;

public record DeleteOccupationalRecommendationsCommand
(
    Guid OccupationalRecommendationId
) : IRequest<ErrorOr<bool>>;

