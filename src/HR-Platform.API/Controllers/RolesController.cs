using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Roles.Common;
using HR_Platform.Application.Roles.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Roles")]
public class Roles(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetByCompanyId")]
    public async Task<IActionResult> GetByCompanyId()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCollaboratorByEmailQuery getCollaboratorByEmailQuery = new(collaboratorEmail);

        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(getCollaboratorByEmailQuery);

        GetRolesByCompanyIdQuery finalRolesQuery = new
        (
            companyResult.Value.Id,
            collaboratorResult.Value.RoleName
        );

        ErrorOr<List<RolesResponse>> rolesResult = await _mediator.Send(finalRolesQuery);

        return rolesResult.Match(
            assignations => Ok(assignations),
            errors => Problem(errors)
        );
    }
}