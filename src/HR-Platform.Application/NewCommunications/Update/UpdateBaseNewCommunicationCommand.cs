using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.NewCommunications.Update;

public record UpdateBaseNewCommunicationCommand
(
    Guid NewCommunicationId,
    string Name,
    string Description,
    bool IsChangedFile,
    IFormFile? File,
    bool IsChangedImage,
    IFormFile? Image,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;


