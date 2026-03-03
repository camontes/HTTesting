using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetNotificationByCollaborator;

public record HideNotificationNoteQuery(string EmailWhoLogIn) : IRequest<ErrorOr<bool>>;