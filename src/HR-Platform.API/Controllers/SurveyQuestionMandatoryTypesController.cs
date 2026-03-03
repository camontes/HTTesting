using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.SurveyQuestionMandatoryTypes.Common;
using HR_Platform.Application.SurveyQuestionMandatoryTypes.GetAll;

namespace HR_Platform.API.Controllers;

[Route("SurveyQuestionMandatoryTypes")]
public class SurveyQuestionMandatoryTypes(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetSurveyQuestionMandatoryTypes")]
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

        ErrorOr<IReadOnlyList<SurveyQuestionMandatoryTypeResponse>> result = await _mediator.Send(new GetAllSurveyQuestionMandatoryTypesQuery());

        return result.Match(
            SurveyQuestionMandatoryType => Ok(SurveyQuestionMandatoryType),
            errors => Problem(errors)
        );
    }
}