using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.TypeAccounts.Common;
using HR_Platform.Application.TypeAccounts.Update;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.TypeAccountEntities.Create;
using HR_Platform.Application.TypeAccounts.Create;
using HR_Platform.Application.TypeAccounts.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.TypeAccounts.Delete;

namespace HR_Platform.API.Controllers;

[Route("TypeAccount")]
public class TypeAccounts(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetTypeAccountsByCompanyId")]
    public async Task<IActionResult> GetTypeAccountsByCompanyId([FromBody] GetBaseTypeAccountsByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetTypeAccountsByCompanyIdQuery finalCollaboratorsQuery = new (
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<TypeAccountsAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseTypeAccountsCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        List<TypeAccountData> pensionDataResult = [];

        if (command is not null && command.TypeAccountEntitiesList is not null && command.TypeAccountEntitiesList.Count > 0)
        {
            foreach (BaseTypeAccountEntityCommand pensionCommand in command.TypeAccountEntitiesList)
            {
                 TypeAccountData pensiondData= new
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

        CreateTypeAccountsCommand createTypeAccountEntitiesCommand = new
        (
            pensionDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createTypeAccountEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateTypeAccountCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateTypeAccountCommand finalCommand = new
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
    public async Task<IActionResult> Delete([FromBody] DeleteBaseTypeAccountCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
        DeleteTypeAccountCommand command = new (
            companyResult.Value.Id,
            baseCommand.TypeAccountList
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );

    }
}