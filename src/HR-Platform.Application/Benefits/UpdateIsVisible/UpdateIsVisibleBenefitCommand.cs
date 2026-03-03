using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Benefits.UpdateIsVisible;

public record UpdateIsVisibleBenefitCommand(Guid Id) : IRequest<ErrorOr<bool>>;