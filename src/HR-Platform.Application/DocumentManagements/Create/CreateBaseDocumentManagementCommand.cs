using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.DocumentManagements.Create;

public record CreateBaseDocumentManagementCommand
(
    Guid CollaboratorId,
    List<IFormFile> Files,
    List<int> FileTypeIds,
    List<string>? Others

) : IRequest<ErrorOr<bool>>;


