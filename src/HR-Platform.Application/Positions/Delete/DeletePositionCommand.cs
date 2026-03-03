using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Positions.Delete;

public record DeletePositionCommand( List<Guid> PositionList) : IRequest<ErrorOr<bool>>;

