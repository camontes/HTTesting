using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ProfessionalAdvices.Delete;
using HR_Platform.Application.ProfessionalAdvices.Update;
using HR_Platform.Application.ProfessionalAdviceEntities.Create;
using HR_Platform.Application.ProfessionalAdvices.Common;
using HR_Platform.Application.ProfessionalAdvices.Create;
using HR_Platform.Application.ProfessionalAdvices.GetByCompanyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("ProfessionalAdvice")]
public class ProfessionalAdvices(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("GetProfessionalAdvicesByCompanyId")]
    public async Task<IActionResult> GetProfessionalAdvicesByCompanyId([FromBody] GetBaseProfessionalAdvicesByCompanyIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetProfessionalAdvicesByCompanyIdQuery finalCollaboratorsQuery = new (
            companyResult.Value.Id,
            baseCollaboratorsQuery.Page,
            baseCollaboratorsQuery.PageSize
            );

        ErrorOr<ProfessionalAdvicesAndCountByCompanyResponse> result = await _mediator.Send(finalCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBaseProfessionalAdvicesCommand command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

          List<ProfessionalAdviceData> professionalAdviceDataResult = [];

        if (command is not null && command.ProfessionalAdviceEntitiesList is not null && command.ProfessionalAdviceEntitiesList.Count > 0)
        {
            foreach (BaseProfessionalAdviceEntityCommand professionalAdviceCommand in command.ProfessionalAdviceEntitiesList)
            {
                 ProfessionalAdviceData professionalAdvicedData= new
                 (
                    companyResult.Value.Id.ToString(),
                    professionalAdviceCommand.Name,
                    professionalAdviceCommand.NameEnglish,
                    professionalAdviceCommand.NameAcronyms,
                    professionalAdviceCommand.NameAcronymsEnglish,
                    true,
                    true
                  );
                professionalAdviceDataResult.Add(professionalAdvicedData);
            }
        }

        CreateProfessionalAdvicesCommand createProfessionalAdviceEntitiesCommand = new
        (
            professionalAdviceDataResult
        );

        ErrorOr<bool> createResult = await _mediator.Send(createProfessionalAdviceEntitiesCommand);

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] BaseUpdateProfessionalAdviceCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        UpdateProfessionalAdviceCommand finalCommand = new
        (
            baseCommand.Id,
            companyResult.Value.Id.ToString(),
            baseCommand.Name,
            baseCommand.Name,
            baseCommand.NameAcronyms,
            baseCommand.NameAcronyms
        );

        ErrorOr<bool> updateResult = await _mediator.Send(finalCommand);

        return updateResult.Match(
            assignation => Ok(assignation),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BaseDeleteProfessionalAdvicesCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteProfessionalAdvicesCommand command = new
        (
            baseCommand.ProfessionalAdvicesList,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            ProfessionalAdvices => Ok(ProfessionalAdvices),
            errors => Problem(errors)
        );
    }

}