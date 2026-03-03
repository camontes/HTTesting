using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.Surveys.Common;
using HR_Platform.Application.Surveys.Create;
using HR_Platform.Application.Surveys.GetByAreaId;
using HR_Platform.Application.Surveys.GetByCompanyId;
using HR_Platform.Application.Surveys.GetById;
using HR_Platform.Application.Surveys.UpdateIsVisibleSurvey;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Surveys")]
public class Surveys
(
    ISender mediator
)
:
ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("CreateSurvey")]
    public async Task<IActionResult> Create([FromBody] CreateBaseSurveyCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCollaboratorByEmailQuery collaboratorQuery = new(collaboratorEmail);

        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(collaboratorQuery);

        collaboratorResult.Match(
            collaborator => Ok(collaborator),
            errors => Problem(errors)
        );

        CreateSurveyCommand result = new
        (
            companyResult.Value.Id,

            command.SurveyTypeId,

            command.Name,

            command.Description,

            collaboratorEmail,
            collaboratorResult.Value.Name,

            command.IsVisible,

            command.SurveyQuestions
        );

        ErrorOr<bool> createResponse = await _mediator.Send(result);

        return createResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetSurveysByCompanyId")]
    public async Task<IActionResult> GetSurveysByCompanyId()
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetSurveyByCompanyIdQuery query = new
        (
            companyResult.Value.Id
        );

        ErrorOr<List<SurveysResponse>> result = await _mediator.Send(query);

        return result.Match(
            surveys => Ok(surveys),
            errors => Problem(errors)
        );
    }


    [HttpPost("GetSurveysByAreaId")]
    public async Task<IActionResult> GetSurveysByAreaId([FromBody] GetBaseSurveyByAreaIdQuery baseQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetSurveyByAreaIdQuery query = new
        (
            companyResult.Value.Id,
            baseQuery.AreaId
        );

        ErrorOr<List<SurveysResponse>> result = await _mediator.Send(query);

        return result.Match(
            surveys => Ok(surveys),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetSurveyById")]
    public async Task<IActionResult> GetSurveyById([FromBody] GetSurveyByIdQuery query)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<SurveyAndQuestionsResponse> result = await _mediator.Send(query);

        return result.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleSurveyById")]
    public async Task<IActionResult> UpdateIsVisibleSurveyById([FromBody] UpdateIsVisibleSurveyCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}