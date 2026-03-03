using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.EventReplays.Common;
using HR_Platform.Application.EventReplays.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("EventReplays")]
public class EventReplays(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetEventReplays")]
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

        ErrorOr<IReadOnlyList<EventReplaysResponse>> EventReplaysResult = await _mediator.Send(new GetAllEventReplaysQuery());

        return EventReplaysResult.Match(
            EventReplays => Ok(EventReplays),
            errors => Problem(errors)
        );
    }
}