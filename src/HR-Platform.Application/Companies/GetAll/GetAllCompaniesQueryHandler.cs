using HR_Platform.Application.Companies.Common;
using ErrorOr;
using HR_Platform.Domain.Companies;
using MediatR;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Application.Companies.GetAll;
internal sealed class GetAllCompaniesQueryHandler(
    ICompanyRepository companyRepository,
    IRoleRepository roleRepository,
    ICollaboratorRepository collaboratorRepository
    ) : IRequestHandler<GetAllCompaniesQuery, ErrorOr<IReadOnlyList<CompaniesResponse>>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<IReadOnlyList<CompaniesResponse>>> Handle(GetAllCompaniesQuery query, CancellationToken cancellationToken)
    {
        IList<Company> companies = await _companyRepository.GetAll();

        if (companies is not null && companies.Count > 0)
        {
            foreach (Company company in companies)
            {
                Role? role = await _roleRepository.GetByCompanyIdAndRoleNameAsync(company.Id, "Superadministrador");

                company.Collaborators = [];

                if (role is not null)
                {
                    Collaborator? superAdmin = await _collaboratorRepository.GetByCompanyAndRoleIdAsync(company.Id, role.Id);

                    if (superAdmin is not null)
                        company.Collaborators.Add(superAdmin);
                }
            }
        }

        #pragma warning disable CS8604 // Posible argumento de referencia nulo

        return companies.Select
        (    company => new CompaniesResponse
            (
                company.Id.Value,
                company.Name,
                company.Collaborators != null && company.Collaborators.Count > 0 ? company.Collaborators[0].Name : string.Empty,
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
            )
        ).ToList();
        
        #pragma warning restore CS8604 // Posible argumento de referencia nulo
    }
}