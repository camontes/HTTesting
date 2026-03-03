using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;

public record UpdateBasicInformationCommand(
    string EmailChangeBy,
    Guid Id,

    IFormFile? PhotoFile,
    string? PhotoURL,
    string? PhotoName

) : IRequest<ErrorOr<UpdateBasicInformationResponse>>;
