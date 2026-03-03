using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Collaborators.Update;
public record UpdateBaseMasterUserCommand(
    string Name,
    string Phone,
    bool IsChangedPhoto,
    IFormFile? PhotoFile
) : IRequest<ErrorOr<bool>>;
