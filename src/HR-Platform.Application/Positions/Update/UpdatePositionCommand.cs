using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Positions.Update;

public record UpdatePositionCommand
(
    Guid Id,
    string Name,
    string? Description,

    string? PositionFile,
    string? PositionFileName

) : IRequest<ErrorOr<bool>>;

