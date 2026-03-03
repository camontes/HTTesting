using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.Create;

public record CreateOccupationalRecommendationsCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<FileOccupationalRecommendationFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



