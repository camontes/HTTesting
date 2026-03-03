using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypeEntities.Create;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ContractTypes.Delete;
using HR_Platform.Application.ContractTypes.GetByCompanyId;
using HR_Platform.Application.ContractTypes.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("ContractType")]
public class ContractTypes(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetContractTypesByCompanyId")]
    public async Task<IActionResult> GetContractTypesByCompanyId([FromBody] GetBaseContractTypesByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetContractTypesByCompanyIdQuery finalCollaboratorsQuery = new (
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<ContractTypesAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseContractTypesCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );


        List<ContractTypeData> ContractTypeDataResult = [];

        if (command is not null && command.ContractTypeEntitiesList is not null && command.ContractTypeEntitiesList.Count > 0)
        {
            foreach (BaseContractTypeEntityCommand ContractTypeCommand in command.ContractTypeEntitiesList)
            {
                 ContractTypeData ContractTypedData= new
                 (
                    companyResult.Value.Id.ToString(),
                    ContractTypeCommand.Name,
                    ContractTypeCommand.NameEnglish,
                    true,
                    true
                  );
                ContractTypeDataResult.Add(ContractTypedData);
            }
        }

        CreateContractTypesCommand createContractTypeEntitiesCommand = new
        (
            ContractTypeDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createContractTypeEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );

    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateContractTypeCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateContractTypeCommand finalCommand = new
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
    public async Task<IActionResult> Delete([FromBody] BaseDeleteContractTypesCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteContractTypesCommand command = new
        (
            baseCommand.ContractTypesList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            ContractTypes => Ok(ContractTypes),
            errors => Problem(errors)
        );
    }
}