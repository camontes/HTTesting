using ErrorOr;
using HR_Platform.Application.EventReplays.Common;
using MediatR;

namespace HR_Platform.Application.EventReplays.GetAll;

public record GetAllEventReplaysQuery() : IRequest<ErrorOr<IReadOnlyList<EventReplaysResponse>>>;