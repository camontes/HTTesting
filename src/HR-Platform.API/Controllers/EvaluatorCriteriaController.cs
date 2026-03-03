using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.EvaluatorCriterias.Common;
using HR_Platform.Application.EvaluatorCriterias.CreateCollaboratorToEvaluator;
using HR_Platform.Application.EvaluatorCriterias.CreateEvalutionByEvaluator;
using HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByEvaluator;
using HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByPositionId;
using HR_Platform.Application.EvaluatorCriterias.NotificationUpdateCriteria;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("EvaluatorCriteria")]
public class EvaluatorCriterias(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


    [HttpGet("GetCollaboratorsByEvaluator")]
    public async Task<IActionResult> GetCollaboratorByEvaluator()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorByEvalutorResponse>> result = await _mediator.Send(new GetCollaboratorByEvaluatorQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("NotificationUpdateCriteria")]
    public async Task<IActionResult> NotificationUpdateCriteria()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(new GetNotificationUpdateCriteriaQuery(companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetCriteriaResultByCollaborator")]
    public async Task<IActionResult> GetAllCollaboratorByPosition(GetBaseCriteriaResultByCollaboratorQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCriteriaResultByCollaboratorQuery request = new
        (
            query.CollaboratorId,
            companyEmail,
            query.IsInHistory
        );

        ErrorOr<List<CriteriaResultByCollaboratorResponse>> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetAllCollaboratorByPosition")]
    public async Task<IActionResult> GetAllCollaboratorByPosition(GetCollaboratorByPositionIdQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorListResponse>> result = await _mediator.Send(query);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("AssignCollaboratorToEvaluator")]
    public async Task<IActionResult> AssignCollaboratorToEvaluator(CreateCollaboratorToEvaluatorCommand command)
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

    [HttpPost("EvalutionByEvaluator")]
    public async Task<IActionResult> EvalutionByEvaluator(CreateBaseEvalutionByEvaluatorCommand command)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        CreateEvalutionByEvaluatorCommand request = new
        (
            companyEmail,
            command.CollaboratorCriteriaId,
            command.PositionName,
            command.PositionNameEnglish,
            command.ObjectiveCriteriaValue,
            command.SubjectiveCriteriaValue,
            command.CriteriaAnswerList,
            command.Comments
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}