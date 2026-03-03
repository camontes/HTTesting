using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Inductions.FinishInduction;

public record FinishInductionCommand(Guid InductionId, string CollaboratorEmail) : IRequest<ErrorOr<bool>>;