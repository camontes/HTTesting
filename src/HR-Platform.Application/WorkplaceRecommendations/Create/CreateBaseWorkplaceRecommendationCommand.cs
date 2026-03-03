using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.WorkplaceRecommendations.Create;

public record CreateBaseWorkplaceRecommendationCommand
(
    Guid CollaboratorId,
    List<IFormFile> Files
) : IRequest<ErrorOr<bool>>;


