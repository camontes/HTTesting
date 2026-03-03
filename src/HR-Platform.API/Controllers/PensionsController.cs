using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.PensionEntities.Create;
using HR_Platform.Application.Pensions.Common;
using HR_Platform.Application.Pensions.Create;
using HR_Platform.Application.Pensions.GetByCompanyId;
using HR_Platform.Application.Pensions.Update;
using HR_Platform.Application.Pensions.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Pension")]
public class Pensions(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetPensionsByCompanyId")]
    public async Task<IActionResult> GetPensionsByCompanyId([FromBody] GetBasePensionsByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetPensionsByCompanyIdQuery finalCollaboratorsQuery = new(
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<PensionsAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBasePensionsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        List<PensionData> pensionDataResult = [];

        if (command is not null && command.PensionEntitiesList is not null && command.PensionEntitiesList.Count > 0)
        {
            foreach (BasePensionEntityCommand pensionCommand in command.PensionEntitiesList)
            {
                PensionData pensiondData = new
                (
                   companyResult.Value.Id.ToString(),
                   pensionCommand.Name,
                   pensionCommand.NameEnglish,
                   true,
                   true
                 );
                pensionDataResult.Add(pensiondData);
            }
        }

        CreatePensionsCommand createPensionEntitiesCommand = new
        (
            pensionDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createPensionEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );

    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdatePensionCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdatePensionCommand finalCommand = new
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
    public async Task<IActionResult> Delete([FromBody] BaseDeletePensionsCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeletePensionsCommand command = new
        (
            baseCommand.PensionsList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            Pensions => Ok(Pensions),
            errors => Problem(errors)
        );
    }
}