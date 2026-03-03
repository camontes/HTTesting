using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateAlreadyLogin;

public record UpdateAlreadyLoginCommand
(
    Guid Id,

    bool AlreadyLogin
) : IRequest<ErrorOr<CollaboratorsResponse>>;
