using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.HealthEntities.Common;
using HR_Platform.Application.HealthEntities.Create;
using HR_Platform.Application.HealthEntities.Delete;
using HR_Platform.Application.HealthEntities.GetByCompanyId;
using HR_Platform.Application.HealthEntities.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("HealthEntities")]
public class HealthEntities(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetHealthEntitiesByCompanyId")]
    public async Task<IActionResult> GetHealthEntitiesByCompanyId([FromBody] GetBaseHealthEntitiesByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetHealthEntitiesByCompanyIdQuery finalHealthEntitiesByCompanyIdQuery = new(
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<List<HealthEntitiesResponse>> result = await _mediator.Send(finalHealthEntitiesByCompanyIdQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BaseCreateHealthEntitiesCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<HealthEntityCommand> healthEntityCommands = [];

        if (command is not null && command.HealthEntitiesList is not null && command.HealthEntitiesList.Count > 0)
        {
            foreach (BaseHealthEntityCommand baseHealthEntityCommand in command.HealthEntitiesList)
            {
                HealthEntityCommand healthEntityCommand = new
                (
                    companyResult.Value.Id.ToString(),

                    baseHealthEntityCommand.Name,
                    baseHealthEntityCommand.NameEnglish,

                    baseHealthEntityCommand.StreetAddress,
                    baseHealthEntityCommand.CountryCode,
                    baseHealthEntityCommand.Country,
                    baseHealthEntityCommand.StateCode,
                    baseHealthEntityCommand.State,
                    baseHealthEntityCommand.CityCode,
                    baseHealthEntityCommand.City,
                    baseHealthEntityCommand.ZipCode,

                    true, // IsEditable
                    true // IsDeleteable
                );

                healthEntityCommands.Add(healthEntityCommand);
            }
        }

        CreateHealthEntitiesCommand createHealthEntitiesCommand = new
        (
            healthEntityCommands
        );

        ErrorOr<bool> createResult = await _mediator.Send(createHealthEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateHealthEntitiesCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateHealthEntitiesCommand finalCommand = new
        (
            baseCommand.Id,
            companyResult.Value.Id.ToString(),
            baseCommand.Name,
            baseCommand.NameEnglish,
            baseCommand.StreetAddress,
            baseCommand.CountryCode,
            baseCommand.Country,
            baseCommand.StateCode,
            baseCommand.State,
            baseCommand.CityCode,
            baseCommand.City,
            baseCommand.ZipCode,
            true, // IsEditable
            true // IsDeleteable
        );

        ErrorOr<bool> updateResult = await _mediator.Send(finalCommand);

        return updateResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BaseDeleteHealthEntitiesCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteHealthEntitiesCommand command = new
        (
            baseCommand.HealthEntitiesList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            HealthEntities => Ok(HealthEntities),
            errors => Problem(errors)
        );
    }
}