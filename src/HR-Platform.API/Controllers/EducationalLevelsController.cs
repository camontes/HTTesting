using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.EducationalLevelEntities.Create;
using HR_Platform.Application.EducationalLevels.Common;
using HR_Platform.Application.EducationalLevels.Create;
using HR_Platform.Application.EducationalLevels.Delete;
using HR_Platform.Application.EducationalLevels.GetByCompanyId;
using HR_Platform.Application.EducationalLevels.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("EducationalLevel")]
public class EducationalLevels(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetEducationalLevelsByCompanyId")]
    public async Task<IActionResult> GetEducationalLevelsByCompanyId([FromBody] GetBaseEducationalLevelsByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetEducationalLevelsByCompanyIdQuery finalCollaboratorsQuery = new (
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<EducationalLevelsAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseEducationalLevelsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<EducationalLevelData> educationalLevelDataResult = [];

        if (command is not null && command.EducationalLevelEntitiesList is not null && command.EducationalLevelEntitiesList.Count > 0)
        {
            foreach (BaseEducationalLevelEntityCommand educationalLevelCommand in command.EducationalLevelEntitiesList)
            {
                EducationalLevelData educationalLevelData = new
                (
                   companyResult.Value.Id.ToString(),
                   educationalLevelCommand.Name,
                   educationalLevelCommand.NameEnglish,
                   true,
                   true
                 );
                educationalLevelDataResult.Add(educationalLevelData);
            }
        }

        CreateEducationalLevelsCommand createEducationalLevelEntitiesCommand = new
        (
            educationalLevelDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createEducationalLevelEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

     [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateEducationalLevelCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateEducationalLevelCommand finalCommand = new
        (
            baseCommand.Id,
            companyResult.Value.Id.ToString(),
            baseCommand.Name,
            baseCommand.Name
        );

        ErrorOr<bool> updateResult = await _mediator.Send(finalCommand);

        return updateResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BaseDeleteEducationalLevelsCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteEducationalLevelsCommand command = new
        (
            baseCommand.EducationalLevelsList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            EducationalLevels => Ok(EducationalLevels),
            errors => Problem(errors)
        );
    }
}