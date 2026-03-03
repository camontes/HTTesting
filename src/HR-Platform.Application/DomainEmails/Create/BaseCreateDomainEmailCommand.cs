using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Create;

public record BaseCreateDomainEmailCommand(
    string DomainEmail,

    bool IsMainDomainEmail
) : IRequest<ErrorOr<Guid>>;
