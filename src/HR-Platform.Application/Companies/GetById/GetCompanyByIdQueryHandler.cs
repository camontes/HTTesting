using ErrorOr;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.Companies.GetById;

internal sealed class GetCompanyByIdQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetCompanyByIdQuery, ErrorOr<CompaniesResponse>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<CompaniesResponse>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.Id)) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        return new CompaniesResponse
        (
            company.Id.Value,
            company.Name,
            company.Collaborators[0].Name,
            company.Collaborators[0].Photo,
            company.Collaborators[0].PhotoName,
            company.Collaborators[0].PhoneNumber,
            company.MenuName,
            company.Address.StreetAddress,
            company.Address.CountryCode,
            company.Address.Country,
            company.Address.StateCode,
            company.Address.State,
            company.Address.CityCode,
            company.Address.City,
            company.Address.ZipCode,
            company.Collaborators[0].BusinessEmail.Value,
            company.Email.Value,
            company.RequestsEmail.Value,
            company.PhoneNumber.Value,
            company.Logo,
            company.LogoName,
            company.URL,
            company.CreationDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }
}