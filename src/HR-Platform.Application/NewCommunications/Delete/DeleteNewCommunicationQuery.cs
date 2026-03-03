using ErrorOr;
using MediatR;

namespace HR_Platform.Application.NewCommunications.Delete;

public record DeleteNewCommunicationQuery(Guid Id) : IRequest<ErrorOr<bool>>;