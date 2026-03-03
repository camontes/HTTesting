using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ActiveBreaks.UpdateIsVisible;

public record UpdateActiveBreakIsVisibleCommand(Guid Id) : IRequest<ErrorOr<bool>>;
