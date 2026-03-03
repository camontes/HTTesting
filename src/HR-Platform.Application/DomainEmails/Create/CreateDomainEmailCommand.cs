using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DomainEmails.Create;

public record CreateDomainEmailCommand(
    string CompanyId,

    string DomainEmail,

    bool IsMainDomainEmail
) : IRequest<ErrorOr<Guid>>;
