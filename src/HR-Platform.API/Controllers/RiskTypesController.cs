using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Risks.Common;
using HR_Platform.Application.Risks.GetAllByRiskType;
using HR_Platform.Application.Risks.UpdateIsVisible;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("RiskType")]
public class RiskType(
    ISender mediator

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetAllRiskTypesByCompanyId")]
    public async Task<IActionResult> GetRiskTypesByCompanyId([FromBody] GetBaseAllQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetAllQuery response = new 
        (
            companyResult.Value.Id,
            baseQuery.IsVisible
        );

        ErrorOr<List<RiskTypeResponse>> result = await _mediator.Send(response);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPatch("UpdateIsVisibleRiskTypeById")]
    public async Task<IActionResult> UpdateIsVisibleRiskTypeById([FromBody] UpdateIsVisibleRiskTypeMainCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}