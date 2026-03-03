using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Assignations.Common;
using HR_Platform.Application.Assignations.Create;
using HR_Platform.Application.Assignations.Delete;
using HR_Platform.Application.Assignations.GetByCompanyIdAndInternalOrExternal;
using HR_Platform.Application.Assignations.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Domain.Assignations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Assignations")]
public class Assignations(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BaseCreateAssignationCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateAssignationElementCommand> assignationsList = [];

        if (baseCommand.AssignationList is not null && baseCommand.AssignationList is not null && baseCommand.AssignationList.Count > 0)
        {
            foreach (BaseCreateAssignationElementCommand assignation in baseCommand.AssignationList)
            {
                CreateAssignationElementCommand newAssignation = new
                (
                    companyResult.Value.Id.ToString(),
                    assignation.Name,
                    assignation.NameEnglish,
                    assignation.IsEditable,
                    assignation.IsDeleteable,
                    assignation.IsInternalAssignation
                );

                assignationsList.Add(newAssignation);
            }
        }

        CreateAssignationCommand createAssignationCommand = new
        (
            assignationsList
        );

        ErrorOr<List<Assignation>> createResult = await _mediator.Send(createAssignationCommand);

        return createResult.Match(
            assignationId => Ok(assignationId),
            errors => Problem(errors)
        );
    }

    [HttpPost("GetByCompanyIdAndInternalOrExternal")]
    public async Task<IActionResult> GetById([FromBody] GetBaseAssignationsByCompanyIdAndInternalOrExternalQuery baseAssignationsQuery)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetAssignationsByCompanyIdAndInternalOrExternalQuery finalAssignationsQuery = new
        (
            companyResult.Value.Id,
            baseAssignationsQuery.IsInternalAssignation
        );

        ErrorOr<List<AssignationsResponse>> assignationsResult = await _mediator.Send(finalAssignationsQuery);

        return assignationsResult.Match(
            assignations => Ok(assignations),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateAssignationCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateAssignationCommand finalCommand = new
        (
            baseCommand.Id,
            companyResult.Value.Id.ToString(),
            baseCommand.Name,
            baseCommand.NameEnglish
        );

        ErrorOr<AssignationsResponse> updateResult = await _mediator.Send(finalCommand);

        return updateResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAssignationCommand baseCommand)
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
            assignation => Ok(assignation),
            errors => Problem(errors)
        );

    }

}