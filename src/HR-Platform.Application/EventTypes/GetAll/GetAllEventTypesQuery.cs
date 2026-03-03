using ErrorOr;
using HR_Platform.Application.EventTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllEventTypesQuery() : IRequest<ErrorOr<IReadOnlyList<EventTypesResponse>>>;