using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Positions.Update;

public record UpdateBasePositionCommand
(
    Guid Id,
    string Name,
    string? Description,

    IFormFile? PositionFile

) : IRequest<ErrorOr<bool>>;

