using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;
public record UpdateBaseBasicInformationCommand(
    Guid? Id,
    IFormFile? PhotoFile

) : IRequest<ErrorOr<UpdateBasicInformationResponse>>;
