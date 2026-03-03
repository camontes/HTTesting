using ErrorOr;
using HR_Platform.Domain.Companies;
using MediatR;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Application.DomainEmails.Common;

namespace HR_Platform.Application.DomainEmails.GetByCompanyId;

internal sealed class GetDomainEmailsByCompanyIdHandler(
    IDomainEmailRepository domainEmailRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetDomainEmailsByCompanyIdQuery, ErrorOr<List<DomainEmailsResponse>>>
{
    private readonly IDomainEmailRepository _domainEmailRepository = domainEmailRepository ?? throw new ArgumentNullException(nameof(domainEmailRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<List<DomainEmailsResponse>>> Handle(GetDomainEmailsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _domainEmailRepository.GetByCompanyIdAsync((new CompanyId(query.CompanyId))) is not List<DomainEmail> domainEmails)
        {
            return Error.NotFound("DomainEmail.NotFound", "The domain emails related with the provide Company Id was not found.");
        }

        List<DomainEmailsResponse> domainEmailsResponses = [];

        if (domainEmails is not null && domainEmails.Count > 0)
        {
            foreach (DomainEmail domainEmail in domainEmails)
            {
                domainEmailsResponses.Add
                (
                    new DomainEmailsResponse
                    (
                        domainEmail.Id.Value,
                        domainEmail.CompanyId.Value,

                        domainEmail.Domain.Value,

                        domainEmail.IsMainDomainEmail
                    )
                );
            }
        }

        return domainEmailsResponses;
    }
}