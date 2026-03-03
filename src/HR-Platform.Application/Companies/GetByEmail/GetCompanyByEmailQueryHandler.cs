using ErrorOr;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.Companies.GetByEmail;

internal sealed class GetCompanyByEmailQueryHandler(
    ICompanyRepository companyRepository,
    ICollaboratorRepository collaboratorRepository
    ) : IRequestHandler<GetCompanyByEmailQuery, ErrorOr<CompaniesResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<CompaniesResponse>> Handle(GetCompanyByEmailQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.Email) is not Collaborator collaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (await _companyRepository.GetByIdAsync(collaborator.CompanyId) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        return new CompaniesResponse
        (
            company.Id.Value,
            company.Name,
            company.Collaborators != null && company.Collaborators .Count > 0 ? company.Collaborators[0].Name : string.Empty,
            company.Collaborators != null && company.Collaborators.Count > 0 ? company.Collaborators[0].Photo : string.Empty, //SuperAdminPhoto
            company.Collaborators != null && company.Collaborators.Count > 0 ? company.Collaborators[0].PhotoName : string.Empty, //SuperAdminPhotoName
            company.Collaborators != null && company.Collaborators.Count > 0 ? company.Collaborators[0].PhoneNumber : string.Empty, // SuperAdmin Phone Number
            company.MenuName,
            company.Address.StreetAddress,
            company.Address.CountryCode,
            company.Address.Country,
            company.Address.StateCode,
            company.Address.State,
            company.Address.CityCode,
            company.Address.City,
            company.Address.ZipCode,
            company.Collaborators != null && company.Collaborators.Count > 0 ? company.Collaborators[0].BusinessEmail.Value : string.Empty,
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