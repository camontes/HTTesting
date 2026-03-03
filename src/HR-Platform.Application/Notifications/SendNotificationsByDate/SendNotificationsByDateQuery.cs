using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Notifications.SendNotificationsByDate;

public record SendNotificationsByDateQuery(DateTime Birthdate) : IRequest<ErrorOr<bool>>;