using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.TalentPoolEntities.Create;
using HR_Platform.Application.TalentPoolEntities.Update;
using HR_Platform.Application.TalentPools.AddCollaborator;
using HR_Platform.Application.TalentPools.Common;
using HR_Platform.Application.TalentPools.Create;
using HR_Platform.Application.TalentPools.DeleteTalentPoolQuery;
using HR_Platform.Application.TalentPools.DuplicateTalentPool;
using HR_Platform.Application.TalentPools.GetByCollaboratorId;
using HR_Platform.Application.TalentPools.GetByCompanyId;
using HR_Platform.Application.TalentPools.GetById;
using HR_Platform.Application.TalentPools.RemoveCollaboratorTalentPool;
using HR_Platform.Application.TalentPools.Update;
using HR_Platform.Application.TalentPools.UpdateArchivedStateById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("TalentPool")]
public class TalentPools(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetTalentPoolsByCompanyId")]
    public async Task<IActionResult> GetTalentPoolsByCompanyId([FromBody] GetBaseTalentPoolsByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetTalentPoolsByCompanyIdQuery finalCollaboratorsQuery = new(
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<TalentPoolsAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateTalentPool")]
    public async Task<IActionResult> Create([FromBody] CreateBaseTalentPoolsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateTalentPoolsCommand result = new
        (
            companyResult.Value.Id,
            command.Tittle,
            command.Description
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPost("AddCollaboratorToTalentPool")]
    public async Task<IActionResult> AddCollaboratorToTalentPool([FromBody] AddCollaboratorTalentPoolCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> createResult = await _mediator.Send(command);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpDelete("RemoveCollaboratorToTalentPool")]
    public async Task<IActionResult> RemoveCollaboratorToTalentPool([FromBody] RemoveCollaboratorTalentPoolQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> removeResult = await _mediator.Send(query);

        return removeResult.Match(
           collaboratorTalentGroupId => Ok(collaboratorTalentGroupId),
           errors => Problem(errors)
       );
    }

    [HttpPost("GetTalentPoolById")]
    public async Task<IActionResult> GetTalentPoolsById([FromBody] GetTalentPoolByIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<TalentPoolByIdResponse> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("DuplicateTalentPool")]
    public async Task<IActionResult> DuplicateTalentPool([FromBody] DuplicateTalentPoolQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetTalentPoolsByCollaboratorId")]
    public async Task<IActionResult> GetTalentPoolsByCollaboratorId([FromBody] GetTalentPoolsByCollaboratorIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<TalentPoolByCollaboratorIdResponse>> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateArchivedStateById")]
    public async Task<IActionResult> UpdateArchivedStateById([FromBody] UpdateArchivedStateByIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpDelete("DeleteTalentPool")]
    public async Task<IActionResult> DeleteTalentPool([FromBody] DeleteTalentPoolQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateTalentPool")]
    public async Task<IActionResult> Update([FromBody] UpdateBaseTalentPoolsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateTalentPoolsCommand result = new
        (
            companyResult.Value.Id,
            command.TalentPoolId,
            command.Tittle,
            command.Description
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
}