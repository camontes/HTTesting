using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.BrigadeMembers.Create;
using HR_Platform.Application.BrigadeMembers.Delete;
using HR_Platform.Application.BrigadeMembers.DeletedBrigadeNotification;
using HR_Platform.Application.BrigadeMembers.GetAllCollaborator;
using HR_Platform.Application.BrigadeMembers.GetByCompanyId;
using HR_Platform.Application.BrigadeMembers.GetGellMember;
using HR_Platform.Application.BrigadeMembers.HasThereBeenUpdate;
using HR_Platform.Application.BrigadeMembers.Update;
using HR_Platform.Application.BrigadeMembers.UpdateIsVisible;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BrigadeMembers")]
public class BrigadeMembers(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetBrigadeMembersByCompanyId")]
    public async Task<IActionResult> GetBrigadeMembersByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BrigadeResponse> result = await _mediator.Send(new GetBrigadeMembersByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetAllCollaboratorsInBrigade")]
    public async Task<IActionResult> GetAllCollaboratorsInBrigade()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorListResponse>> result = await _mediator.Send(new GetAllCollaboratorQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("HasThereBeenUpdate")]
    public async Task<IActionResult> HasThereBeenUpdate()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new HasThereBeenUpdateQuery());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpGet("DeletedBrigadeNotification")]
    public async Task<IActionResult> DeletedBrigadeNotification()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new DeletedBrigadeNotificationQuery());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetAllBrigadeMembers")]
    public async Task<IActionResult> GetAllBrigadeMembers()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<BrigadeCommunication> result = await _mediator.Send(new GetAllMemberQuery());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseBrigadeMembersCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateBrigadeMembersCommand result = new
        (
            command.BrigadeMembersDataList,
            companyResult.Value.Id
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpDelete("DeleteAllBrigadeMember")]
    public async Task<IActionResult> DeleteBrigadeMembers()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> createResult = await _mediator.Send(new DeleteBrigadeMembersCommand());

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateBaseBrigadeMembersCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateBrigadeMembersCommand result = new
        (
            command.BrigadeMembersDataList,
            companyResult.Value.Id
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch("UpdateIsVisibleBrigadeMembers")]
    public async Task<IActionResult> UpdateIsVisibleBrigadeMembers()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new UpdateIsVisibleBrigadeMembersCommand());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}