using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.WorkplaceEvidences.Create;

public record CreateBaseWorkplaceEvidenceCommand
(
    string? CollaboratorId,
    List<IFormFile> Files
) : IRequest<ErrorOr<bool>>;


