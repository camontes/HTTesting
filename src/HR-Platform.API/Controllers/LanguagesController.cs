using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Languages.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Languages")]
public class Languages(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetLanguageNames")]
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

        ErrorOr<IReadOnlyList<LanguagesResponse>> LanguagesResult = await _mediator.Send(new GetAllLanguagesNamesQuery());

        return LanguagesResult.Match(
            Languages => Ok(Languages),
            errors => Problem(errors)
        );
    }
    
    [HttpGet("GetLanguageLevels")]
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

        ErrorOr<IReadOnlyList<LanguagesResponse>> LanguagesResult = await _mediator.Send(new GetAllLanguagesLevelsQuery());

        return LanguagesResult.Match(
            Languages => Ok(Languages),
            errors => Problem(errors)
        );
    }
}