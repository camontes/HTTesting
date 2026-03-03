using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Positions.Create;

public record CreatePositionsCommand(
    string CompanyId,

    string Name,
    string NameEnglish,

    string? Description,
    string? DescriptionEnglish,

    string? PositionURL,
    string? PositionFileName,

    bool IsEditable,
    bool IsDeleteable
) : IRequest<ErrorOr<Guid>>;

