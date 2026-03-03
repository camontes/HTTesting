using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ActiveBreaks.Update;

public record UpdateBaseActiveBreakCommand
(
    Guid Id,

    string Name,

    string Description,

    IFormFile? Image,
    IFormFile? File,

    string? ImageName,
    string? FileName,

    string? ImageURL,
    string? FileURL
)
:
IRequest<ErrorOr<bool>>;



