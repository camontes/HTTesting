using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.TechnologyTools.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("TechnologyTools")]
public class TechnologyTools(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetTechnologyNames")]
    public async Task<IActionResult> GetAllNames()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<IReadOnlyList<TechnologyToolsResponse>> TechnologyNamesResult = await _mediator.Send(new GetAllTechnologyNamesQuery());

        return TechnologyNamesResult.Match(
            TechnologyNames => Ok(TechnologyNames),
            errors => Problem(errors)
        );
    }
    
    [HttpGet("GetTKnowledgeLevels")]
    public async Task<IActionResult> GetAllLevels()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<IReadOnlyList<TechnologyToolsResponse>> KnowledgeLevelsResult = await _mediator.Send(new GetAllKnowledgeLevelsQuery());

        return KnowledgeLevelsResult.Match(
            TechnologyLevels => Ok(TechnologyLevels),
            errors => Problem(errors)
        );
    }
}