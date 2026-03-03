using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.DomainEmails.Delete;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.DomainEmails.Common;
using HR_Platform.Application.DomainEmails.Create;
using HR_Platform.Application.DomainEmails.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.DomainEmails.Update;

namespace HR_Platform.API.Controllers;

[Route("DomainEmails")]
public class DomainEmails(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BaseCreateDomainEmailCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateDomainEmailCommand newDomainEmail = new
        (
            companyResult.Value.Id.ToString(),
            command.DomainEmail,
            command.IsMainDomainEmail

        );

        ErrorOr<Guid> createResult = await _mediator.Send(newDomainEmail);

        return createResult.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetByCompanyId")]
    public async Task<IActionResult> GetByCompanyId()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetDomainEmailsByCompanyIdQuery finalDomainEmailsQuery = new
        (
            companyResult.Value.Id
        );

        ErrorOr<List<DomainEmailsResponse>> domainEmailsResult = await _mediator.Send(finalDomainEmailsQuery);

        return domainEmailsResult.Match(
            domainEmails => Ok(domainEmails),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateDomainEmailCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> updateResult = await _mediator.Send(baseCommand);

        return updateResult.Match(
            domainEmail => Ok(domainEmail),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteDomainEmailCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> updateResult = await _mediator.Send(baseCommand);

        return updateResult.Match(
            domainEmail => Ok(domainEmail),
            errors => Problem(errors)
        );

    }
}