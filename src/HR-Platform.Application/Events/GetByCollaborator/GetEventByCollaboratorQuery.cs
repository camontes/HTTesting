using ErrorOr;
using HR_Platform.Application.Events.Common;
using MediatR;

namespace HR_Platform.Application.Events.GetByCollaborator;

public record GetEventByCollaboratorQuery(string CollaboratorEmail) : IRequest<ErrorOr<List<EventResponse>>>;