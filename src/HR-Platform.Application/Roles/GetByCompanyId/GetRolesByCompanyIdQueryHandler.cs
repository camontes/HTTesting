using ErrorOr;
using HR_Platform.Domain.Companies;
using MediatR;
using HR_Platform.Application.Roles.Common;
using HR_Platform.Domain.Roles;

namespace HR_Platform.Application.Roles.GetByCompanyId;

internal sealed class GetRolesByCompanyIdHandler(
    IRoleRepository roleRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetRolesByCompanyIdQuery, ErrorOr<List<RolesResponse>>>
{
    private readonly IRoleRepository _roleRepository     = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<List<RolesResponse>>> Handle(GetRolesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        List<Role> roles;

        if (query.RoleName != "SuperAdministrador")
            roles = await _roleRepository.GetByCompanyIdAsync((new CompanyId(query.CompanyId)));
        else
            roles = await _roleRepository.GetByCompanyIdWithoutSuperAdminAsync((new CompanyId(query.CompanyId)));

        if(roles == null || roles.Count == 0)
        {
            return Error.NotFound("Role.NotFound", "The roles related with the provide Company Id was not found.");
        }

        List<RolesResponse>? rolesResponse = [];

        if (roles is not null && roles.Count > 0)
        {
            foreach (Role role in roles)
            {
                rolesResponse.Add
                (
                    new RolesResponse
                    (
                        role.Id.Value,
                        role.CompanyId.Value,

                        role.Name,
                        role.NameEnglish,

                        role.IsEditable,
                        role.IsDeleteable,

                        role.CreationDate.Value,
                        role.EditionDate.Value
                    )
                );
            }
        }

        return rolesResponse;
    }
}