using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Tags.Common;
using HR_Platform.Application.Tags.Create;
using HR_Platform.Application.Tags.Delete;
using HR_Platform.Application.Tags.GetByCompanyId;
using HR_Platform.Application.Tags.Update;
using HR_Platform.Application.Tags.AddCollaborator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.Tags.GetByCollaborator;
using HR_Platform.Application.Tags.DeleteFromResume;
using HR_Platform.Application.Tags.CreateFromResume;

namespace HR_Platform.API.Controllers;

[Route("Tag")]
public class Tags(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetTagsByCompanyId")]
    public async Task<IActionResult> GetTagsByCompanyId([FromBody] GetBaseTagsByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetTagsByCompanyIdQuery finalCollaboratorsQuery = new (
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<TagsAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("Tags")]
    public async Task<IActionResult> Create([FromBody] CreateBaseTagsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        List<TagData> tagDataResult = [];

        if (command is not null && command.TagsList is not null && command.TagsList.Count > 0)
        {
            foreach (BaseTagCommand tagCommand in command.TagsList)
            {
                 TagData tagdData= new
                 (
                    companyResult.Value.Id.ToString(),
                    tagCommand.Name
                  );
                tagDataResult.Add(tagdData);
            }
        }

        CreateTagsCommand createTagsCommand = new
        (
            tagDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createTagsCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );

    }

    [HttpPost("AddCollaboratorToTag")]
    public async Task<IActionResult> AddCollaboratorToTag([FromBody] AddCollaboratorTagCommand command)
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
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpDelete("DeleteFromResume")]
    public async Task<IActionResult> DeleteFromResume([FromBody] DeleteFromResumeCommand command)
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
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPost("GetTagsByCollaborator")]
    public async Task<IActionResult> GetTagsByCollaborator([FromBody] GetByCollaboratorQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<TagNamesResponse>> result = await _mediator.Send(query);

        return result.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateTagCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateTagCommand uploadCommand = new
        (
            command.Id,

            companyResult.Value.Id.ToString(),

            command.Name,
            command.NameEnglish
        );

        ErrorOr<TagsResponse> updateResult = await _mediator.Send(uploadCommand);

        return updateResult.Match(
            tag => Ok(tag),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BaseDeleteTagsCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteTagsCommand command = new
        (
            baseCommand.TagsList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            Tags => Ok(Tags),
            errors => Problem(errors)
        );
    }
    
    [HttpPost("CreateTagFromResume")]
    public async Task<IActionResult> CreateTagFromResume([FromBody] CreateBaseFromResumeTagsCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateFromResumeTagsCommand command = new
        (
            baseCommand.Name,
            baseCommand.CollaboratorId,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            Tags => Ok(Tags),
            errors => Problem(errors)
        );
    }
}