using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeDocumentations.Create;

public record CreateBrigadeDocumentationsCommand(
    string EmailChangeBy,
    Guid CompanyId,
    List<CreateBrigadeDocumentationsObjectCommand> BrigadeDocumentationsList
) : IRequest<ErrorOr<bool>>;

public record CreateBrigadeDocumentationsObjectCommand(
    IFormFile BrigadeDocumentationFile,
    string FileName,
    string UrlFile
);

