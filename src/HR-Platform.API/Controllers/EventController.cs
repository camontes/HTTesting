using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Events.Common;
using HR_Platform.Application.Events.Create;
using HR_Platform.Application.Events.GetByCollaborator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Event")]
public class Events(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseEventCommand command)
    {

        Token token = new();
        string emailWhoIsLogin = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(emailWhoIsLogin);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<Guid> temp = [Guid.Parse("00000000-0000-0000-0000-000000000000")];

        CreateEventCommand result = new
        (
            companyResult.Value.Id,
            emailWhoIsLogin,
            command.EventName,
            command.StartDate,
            command.StartTime,
            command.EndDate,
            command.EndTime,
            command.EventTypeId,
            command.TimeZoneId,
            command.EventRecurrenceId,
            command.Description,
            command.SendForAll,
            command.CollaboratorIds
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }


    [HttpGet("GetAllEventByCollaborator")]
    public async Task<IActionResult> GetEvent()
    {

        Token token = new();
        string emailWhoIsLogin = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(emailWhoIsLogin);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<EventResponse>> createResult = await _mediator.Send(new GetEventByCollaboratorQuery(emailWhoIsLogin));

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
}