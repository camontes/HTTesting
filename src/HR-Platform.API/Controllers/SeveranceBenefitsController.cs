using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.SeveranceBenefits.Common;
using HR_Platform.Application.SeveranceBenefits.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.SeveranceBenefitEntities.Create;
using HR_Platform.Application.SeveranceBenefits.Create;
using HR_Platform.Application.SeveranceBenefits.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.SeveranceBenefits.Delete;

namespace HR_Platform.API.Controllers;

[Route("SeveranceBenefit")]
public class SeveranceBenefits(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetSeveranceBenefitsByCompanyId")]
    public async Task<IActionResult> GetSeveranceBenefitsByCompanyId([FromBody] GetBaseSeverancesByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetSeveranceBenefitsByCompanyIdQuery finalCollaboratorsQuery = new(
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<SeveranceBenefitWithCountResponse> result = await _mediator.Send(finalCollaboratorsQuery);


        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseSeveranceBenefitsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<SeveranceBenefitData> severanceBenefitDataResult = [];

        if (command is not null && command.SeveranceBenefitEntitiesList is not null && command.SeveranceBenefitEntitiesList.Count > 0)
        {
            foreach (BaseSeveranceBenefitEntityCommand severanceBenefitCommand in command.SeveranceBenefitEntitiesList)
            {
                SeveranceBenefitData severanceBenefitdData = new
                (
                   companyResult.Value.Id.ToString(),
                   severanceBenefitCommand.Name,
                   severanceBenefitCommand.NameEnglish,
                   true,
                   true
                 );
                severanceBenefitDataResult.Add(severanceBenefitdData);
            }
        }

        CreateSeveranceBenefitsCommand createSeveranceBenefitEntitiesCommand = new
        (
            severanceBenefitDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createSeveranceBenefitEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );

    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateSeveranceBenefitCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateSeveranceBenefitCommand finalCommand = new
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
    public async Task<IActionResult> Delete([FromBody] BaseDeleteSeveranceBenefitsCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteSeveranceBenefitsCommand command = new
        (
            baseCommand.SeveranceBenefitsList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            SeveranceBenefits => Ok(SeveranceBenefits),
            errors => Problem(errors)
        );
    }
}