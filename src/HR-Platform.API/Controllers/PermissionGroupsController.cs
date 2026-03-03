using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.PermissionGroups.Common;
using HR_Platform.Application.PermissionGroups.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("PermissionGroups")]
public class PermissionGroups(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetWithPermissions")]
    public async Task<IActionResult> GetWithPermissions(GetAllPermissionGroupsQuery getAllPermissionGroupsQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<IReadOnlyList<PermissionGroupsResponse>> permissonGroupsResult = await _mediator.Send(new GetAllPermissionGroupsQuery(getAllPermissionGroupsQuery.RoleId));

        return permissonGroupsResult.Match(
            permissonGroups => Ok(permissonGroups),
            errors => Problem(errors)
        );
    }
}