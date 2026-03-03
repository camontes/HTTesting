using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Inductions.Delete;

public record DeleteBaseInductionCommand(Guid InductionId) : IRequest<ErrorOr<bool>>;