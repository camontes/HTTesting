using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Update;

public record UpdateDomainEmailCommand(Guid Id, string DomainEmail) : IRequest<ErrorOr<bool>>;