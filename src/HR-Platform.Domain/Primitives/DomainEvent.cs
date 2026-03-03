using MediatR;

namespace HR_Platform.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;