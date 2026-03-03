using ErrorOr;
using HR_Platform.Application.Notifications.Common;
using MediatR;

namespace HR_Platform.Application.Notifications.GetAllClaimsByCompanyId;

public record GetAllByCollaboratorIdQuery(string CollaboratorEmail) : IRequest<ErrorOr<NotificationResponse>>;