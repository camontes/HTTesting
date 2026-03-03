using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Inductions.FinishInduction;

public record BaseFinishInductionCommand(Guid InductionId) : IRequest<ErrorOr<bool>>;