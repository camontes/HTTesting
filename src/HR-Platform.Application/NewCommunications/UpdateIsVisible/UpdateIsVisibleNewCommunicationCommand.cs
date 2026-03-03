using ErrorOr;
using MediatR;

namespace HR_Platform.Application.NewCommunications.UpdateIsVisible;

public record UpdateIsVisibleNewCommunicationCommand(Guid Id) : IRequest<ErrorOr<bool>>;