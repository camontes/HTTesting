using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.OccupationalRecommendations.Create;

public record CreateBaseOccupationalRecommendationCommand
(
    Guid CollaboratorId,
    List<IFormFile> Files
) : IRequest<ErrorOr<bool>>;


