using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.Create;

public record CreateActiveBreakCommand(
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


