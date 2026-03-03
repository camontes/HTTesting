using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.Create;

public record CreateWorkplaceRecommendationsCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<FileWorkplaceRecommendationFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



