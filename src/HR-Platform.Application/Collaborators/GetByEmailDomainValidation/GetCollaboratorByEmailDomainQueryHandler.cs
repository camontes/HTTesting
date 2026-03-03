using ErrorOr;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Text.RegularExpressions;

namespace HR_Platform.Application.Collaborators.GetByEmail;

internal sealed class GetCollaboratorByEmailDomainDomainQueryHandler(
        IDomainEmailRepository domainEmailRepository
        ) : IRequestHandler<GetCollaboratorByEmailDomainQuery, ErrorOr<bool>>
    {
    private readonly IDomainEmailRepository _domainEmailRepository = domainEmailRepository ?? throw new ArgumentNullException(nameof(domainEmailRepository));

    public async Task<ErrorOr<bool>> Handle(GetCollaboratorByEmailDomainQuery query, CancellationToken cancellationToken)
    {
        const string regex = @"@(?<domain>\w+\.\w+(?:\.\w+)*)";
        Match match = Regex.Match(query.Email, regex);

        if (MailDomain.Create(match.Success ? match.Value : string.Empty) is not MailDomain domain)
            return Error.Validation("Collaborators.DomainEmail", "DomainEmail has not valid format");

        bool isDomainFound = await _domainEmailRepository.ExistsDomainNameAsync(domain);

        if (!isDomainFound)
            return Error.Validation("Collaborators.DomainEmail", "This domain is not available");

        return true;
    }
}