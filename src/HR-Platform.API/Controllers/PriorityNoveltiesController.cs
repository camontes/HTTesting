using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.PriorityNovelties.Common;
using HR_Platform.Application.PriorityNovelties.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("PriorityNovelties")]
public class PriorityNovelties(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet()]
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

        GetAllPriorityNoveltiesQuery getAllPriorityNoveltiesQuery = new();

        ErrorOr<List<PriorityNoveltiesResponse>> result = await _mediator.Send(getAllPriorityNoveltiesQuery);

        return result.Match(
            defaultFamilyCompositions => Ok(defaultFamilyCompositions),
            errors => Problem(errors));
    }
}