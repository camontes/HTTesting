using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.DreamMapAnswers.Common;
using HR_Platform.Application.DreamMapAnswers.Create;
using HR_Platform.Application.DreamMapAnswers.DeleteDreamMap;
using HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;
using HR_Platform.Application.DreamMapAnswers.GetByCompanyId;
using HR_Platform.Application.DreamMapAnswers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("DreamMapAnswer")]
public class DreamMapAnswers(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("DreamMapAnswersCollaborators")]
    public async Task<IActionResult> DreamMapAnswersCollaborators()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<DreamMapAnswersCollaboratorResponse>> result = await _mediator.Send(new GetDreamMapAnswersByCompanyIdQuery());

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetUpdateQuestionNotification")]
    public async Task<IActionResult> GetUpdateQuestionNotification()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new GetUpdateQuestionNotificationQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("HideNotificationByQuestionUpdate")]
    public async Task<IActionResult> HideNotificationByQuestionUpdate()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new HideNotificationByQuestionUpdateQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetDreamMapAnswersByCollaboratorId")]
    public async Task<IActionResult> GetDreamMapAnswersByCollaboratorId([FromBody] GetBaseDreamMapAnswersByCollaboratorIdQuery command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetDreamMapAnswersByCollaboratorIdQuery commandNew = new
        (
            string.IsNullOrEmpty(command.CollaboratorById) ? companyEmail : command.CollaboratorById,
            string.IsNullOrEmpty(command.CollaboratorById)
        );

        ErrorOr<DreamMapAnswersResponse> result = await _mediator.Send(commandNew);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateDreamMapAnswer")]
    public async Task<IActionResult> Create([FromBody] CreateBaseDreamMapAnswersCommand command)
    {

        Token token = new();
        string CollaboratorEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(CollaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateDreamMapAnswersCommand result = new
        (
            CollaboratorEmail,
            command.DreamMapAnswersDataList,
            command.TemplateIndicator
        );
        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch("UpdateDreamMapAnswer")]
    public async Task<IActionResult> Update([FromBody] UpdateBaseDreamMapAnswersCommand command)
    {

        Token token = new();
        string CollaboratorEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(CollaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateDreamMapAnswersCommand result = new
        (
            CollaboratorEmail,
            command.DreamMapAnswersDataList,
            command.TemplateIndicator
        );
        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpDelete("DeleteDreamMap")]
    public async Task<IActionResult> DeleteDreamMap(DeleteDreamMapCommand command)
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