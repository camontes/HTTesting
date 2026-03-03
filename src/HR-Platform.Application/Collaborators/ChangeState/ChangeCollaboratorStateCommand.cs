using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.ChangeState;

public record ChangeCollaboratorStateCommand
(
    string Id,

    string? SuspensionReason,

    bool IsSuspended,

    int CollaboratorStateId

) : IRequest<ErrorOr<CollaboratorsResponse>>;
