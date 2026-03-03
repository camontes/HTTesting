using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.MaritalStatuses.Common;
using HR_Platform.Application.MaritalStatuses.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("MaritalStatuses")]
public class MaritalStatuses(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> GeAll()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetAllMaritalStatusesQuery getAllMaritalStatusesQuery = new();

        ErrorOr<List<MaritalStatusesResponse>> result = await _mediator.Send(getAllMaritalStatusesQuery);

        return result.Match(
            maritalStatuses => Ok(maritalStatuses),
            errors => Problem(errors));
    }
}