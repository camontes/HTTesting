using ErrorOr;
using HR_Platform.Application.Notes.Common;
using MediatR;

namespace HR_Platform.Application.Notes.GetNotificationByCollaborator;

public record GetNotificationByCollaboratorQuery(string EmailWhoLogIn) : IRequest<ErrorOr<NotificationNotesResponse>>;