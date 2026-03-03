using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.AssignationTypes.Common;
using HR_Platform.Application.AssignationTypes.GetAll;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("AssignationTypes")]
public class AssignationTypes(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
        company => Ok(company),
        errors => Problem(errors)
        );

        ErrorOr<IReadOnlyList<AssignationTypesResponse>> assignationTypesResult = await _mediator.Send(new GetAllAssignationTypesQuery());

        return assignationTypesResult.Match(
            assignationTypes => Ok(assignationTypes),
            errors => Problem(errors)
        );
    }
}