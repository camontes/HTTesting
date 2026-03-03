using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Risks.UpdateIsVisible;

public record UpdateIsVisibleCommand(Guid Id) : IRequest<ErrorOr<bool>>;