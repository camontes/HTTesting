using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.QuestionTypes.Common;
using HR_Platform.Application.QuestionTypes.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("QuestionTypes")]
public class QuestionTypes(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetQuestionTypes")]
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

        ErrorOr<List<QuestionTypesResponse>> QuestionTypesResult = await _mediator.Send(new GetQuestionTypesByCompanyIdQuery(companyResult.Value.Id));

        return QuestionTypesResult.Match(
            QuestionTypes => Ok(QuestionTypes),
            errors => Problem(errors)
        );
    }
}