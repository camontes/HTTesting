using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.Update;

public record UpdateActiveBreakCommand(
    Guid Id, 
    
    string Name,

    string Description,

    string? ImageName,
    string? ImageURL,

    string? FileName,
    string? FileURL,

    string EmailWhoChangedByHR,

    Guid CompanyId
)
:
IRequest<ErrorOr<bool>>;


