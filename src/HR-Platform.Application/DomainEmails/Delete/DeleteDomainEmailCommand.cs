using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Delete;

public record DeleteDomainEmailCommand(Guid Id) : IRequest<ErrorOr<bool>>;