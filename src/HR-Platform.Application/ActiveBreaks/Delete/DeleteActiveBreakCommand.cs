using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.Delete;

public record DeleteActiveBreakCommand(Guid Id) : IRequest<ErrorOr<bool>>;
