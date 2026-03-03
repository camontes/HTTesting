using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Inductions.UpdateIsVisible;

public record UpdateIsVisibleInductionCommand(Guid InductionId, bool IsVisible, bool AllowForAllCollaborators, List<Guid>? CollaboratorIds) : IRequest<ErrorOr<bool>>;