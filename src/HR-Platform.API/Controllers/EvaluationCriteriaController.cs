using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.EvaluationCriterias.Common;
using HR_Platform.Application.EvaluationCriterias.CreateCriterias;
using HR_Platform.Application.EvaluationCriterias.CreateDefaultCriteriasByPosition;
using HR_Platform.Application.EvaluationCriterias.GetByPosition;
using HR_Platform.Application.EvaluationCriterias.UpdateCriteriaByPosition;
using HR_Platform.Application.EvaluationCriterias.UpdateGeneralCriteriaByPosition;
using HR_Platform.Application.EvaluationCriteriaScores.UpdateByEvaluationCriteria;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("EvaluationCriteria")]
public class EvaluationCriterias(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetEvaluationCriteriaByPosition")]
    public async Task<IActionResult> GetEvaluationCriteria(GetEvaluationCriteriaByPositionQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<EvaluationCriteriaResponse> result = await _mediator.Send(query);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPatch("UpdateGeneralCriteriaByPosition")]
    public async Task<IActionResult> UpdateGeneralCriteriaByPosition(UpdateGeneralCriteriaByPositionCommand command)
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

    [HttpPatch("UpdateCriteriaByPosition")]
    public async Task<IActionResult> UpdateCriteriaByRole(UpdateCriteriaByPositionCommand command)
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

    [HttpPost("CreateDefaultCriteriasByPositionId")]
    public async Task<IActionResult> CreateDefaultCriteriasByPositionId(CreateDefaultCriteriasByPositionCommand command)
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

    [HttpPatch("UpdateCriteriaScoresByEvaluationCriteria")]
    public async Task<IActionResult> UpdateScoresByEvaluationCriteria(UpdateCriteriaScoresByCriteriaCommand command)
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

    [HttpPost("CreateCriteria")]
    public async Task<IActionResult> CreateCriteria(CreateCriteriasCommand command)
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