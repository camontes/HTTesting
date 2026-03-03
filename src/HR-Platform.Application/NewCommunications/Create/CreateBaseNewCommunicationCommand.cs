using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.NewCommunications.Create;

public record CreateBaseNewCommunicationCommand
(
    string Name,
    string Description,
    IFormFile? File,
    IFormFile? Image,
    bool IsSurveyInclude
) : IRequest<ErrorOr<bool>>;


