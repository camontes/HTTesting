using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.DeletedBrigadeNotification;

public record DeletedBrigadeNotificationQuery() : IRequest<ErrorOr<bool>>;