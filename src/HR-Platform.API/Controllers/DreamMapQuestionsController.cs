using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.DreamMapQuestionEntities.Create;
using HR_Platform.Application.DreamMapQuestionEntities.Update;
using HR_Platform.Application.DreamMapQuestions.Common;
using HR_Platform.Application.DreamMapQuestions.Create;
using HR_Platform.Application.DreamMapQuestions.DeleteAll;
using HR_Platform.Application.DreamMapQuestions.GetByCompanyId;
using HR_Platform.Application.DreamMapQuestions.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("DreamMapQuestion")]
public class DreamMapQuestions(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("GetDreamMapQuestionsByCompanyId")]
    public async Task<IActionResult> GetDreamMapQuestionsByCompanyId()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<DreamMapQuestionsResponse>> result = await _mediator.Send(new GetDreamMapQuestionsByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateDreamMapQuestion")]
    public async Task<IActionResult> Create([FromBody] CreateBaseDreamMapQuestionsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateDreamMapQuestionsCommand result = new
        (
            companyResult.Value.Id,
            command.DreamMapQuestionList
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }


    [HttpPatch("UpdateDreamMapQuestion")]
    public async Task<IActionResult> Update([FromBody] UpdateBaseDreamMapQuestionsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateDreamMapQuestionsCommand result = new
        (
            companyResult.Value.Id,
            command.DreamMapQuestionList
        );

        ErrorOr<bool> createResult = await _mediator.Send(result);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpDelete("DeleteDreamMapQuestionsByCompanyId")]
    public async Task<IActionResult> DeleteDreamMapQuestionsByCompanyId()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new DeleteAllQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}