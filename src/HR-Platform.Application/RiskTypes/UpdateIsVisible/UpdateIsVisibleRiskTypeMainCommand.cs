using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Risks.UpdateIsVisible;

public record UpdateIsVisibleRiskTypeMainCommand(Guid Id) : IRequest<ErrorOr<bool>>;