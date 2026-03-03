using ErrorOr;
using HR_Platform.Application.WorkplaceInformations.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.WorkplaceInformations.Create;

public record CreateBaseWorkplaceInformationCommand
(
    Guid CollaboratorId,
    List<IFormFile> Files
) : IRequest<ErrorOr<bool>>;


