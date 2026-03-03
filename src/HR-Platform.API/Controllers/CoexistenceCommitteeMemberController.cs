using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.CoexistenceCommitteeMembers.Common;
using HR_Platform.Application.CoexistenceCommitteeMembers.Create;
using HR_Platform.Application.CoexistenceCommitteeMembers.GetAllCollaborator;
using HR_Platform.Application.CoexistenceCommitteeMembers.GetAllMembers;
using HR_Platform.Application.CoexistenceCommitteeMembers.RemoveMember;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("CoexistenceCommitteeMembers")]
public class CoexistenceCommitteeMembers(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetCoexistenceCommitteeMembers")]
    public async Task<IActionResult> GetCoexistenceCommitteeMembersByCompanyId()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CoexistenceCommitteeMemberResponse>> result = await _mediator.Send(new GetCoexistenceCommitteeMemberQuery());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetAllCollaboratorsInCoexistenceCommittee")]
    public async Task<IActionResult> GetAllCollaboratorsInCoexistenceCommittee()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorCoexistenceCommitteeListResponse>> result = await _mediator.Send(new GetAllCollaboratorCoexistenceCommitteeQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("IntegrateCollaboratorToCoexistenceCommittee")]
    public async Task<IActionResult> IntegrateCollaboratorToCoexistenceCommittee(CreateCoexistenceCommitteeMembersCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(command);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("RemoveCoexistenceCommitteeMember")]
    public async Task<IActionResult> RemoveCoexistenceCommitteeMember(RemoveCoexistenceCommitteeMemberCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(command);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}