using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeDocumentations.Create;

public record CreateBaseBrigadeDocumentationCommand
(
    List<IFormFile> BrigadeDocumentationFiles
) : IRequest<ErrorOr<bool>>;


