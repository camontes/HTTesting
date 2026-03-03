using ErrorOr;
using HR_Platform.Application.RepeatEvents.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllRepeatEventsQuery() : IRequest<ErrorOr<RepeatEventsResponse>>;