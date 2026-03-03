using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ActiveBreaks.Create;

public record CreateBaseActiveBreakCommand
(
    string Name,

    string Description,
    
    IFormFile? Image,
    IFormFile? File
)
:
IRequest<ErrorOr<bool>>;


