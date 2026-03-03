using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.Inductions.Create;
using HR_Platform.Application.Inductions.Delete;
using HR_Platform.Application.Inductions.FinishInduction;
using HR_Platform.Application.Inductions.GetActiveCollaboratorByInductionId;
using HR_Platform.Application.Inductions.GetAllCollaborator;
using HR_Platform.Application.Inductions.GetByCollaboratorId;
using HR_Platform.Application.Inductions.GetByCompanyId;
using HR_Platform.Application.Inductions.InductionCompleted;
using HR_Platform.Application.Inductions.InductionSent;
using HR_Platform.Application.Inductions.Update;
using HR_Platform.Application.Inductions.UpdateIsVisible;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("Inductions")]
public class Induction(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpGet("GetInductionsByCompanyId")]
    public async Task<IActionResult> GetInductionsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetInductionByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id
        );

        ErrorOr<List<InductionResponse>> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetInductionsSent")]
    public async Task<IActionResult> GetInductionSent()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<InductionSentResponse>> result = await _mediator.Send(new GetInductionSentQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetInductionCompleted")]
    public async Task<IActionResult> GetInductionCompleted()
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<InductionCompletedResponse>> result = await _mediator.Send(new GetInductionCompletedQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateInductionFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseInductionCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateInductionsObjectCommand> formatFiles = [];

        if (command.InductionFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.InductionFiles.Count; i++)
            {
                string fileNameAux = command.InductionFiles[i].FileName
                    .Replace("\\", "-")
                    .Replace("/", "-")
                    .Replace(":", "-")
                    .Replace("*", "-")
                    .Replace("?", "-")
                    .Replace("\"", "-")
                    .Replace("<", "-")
                    .Replace(">", "-")
                    .Replace("|", "-")
                    .Replace("#", "-")
                    .Replace("$", "-");

                fileURL = await _amazonS3Service.UploadFile(command.InductionFiles[i], "InductionsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateInductionsObjectCommand temp = new
                (
                    command.InductionFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateInductionsCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            command.Name,
            command.Description,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleInductionById")]
    public async Task<IActionResult> UpdateIsVisibleInductionById([FromBody] UpdateIsVisibleInductionCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetActiveCollaboratorsByInductionId")]
    public async Task<IActionResult> GetActiveCollaboratorsByInductionId(GetActiveCollaboratorByInductionIdQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorActiveResponse>> result = await _mediator.Send(query);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPatch("UpdateInduction")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseInductionCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<UpdateInductionsObjectCommand> formatFiles = [];

        if (command.InductionFiles is not null && command.HasChangedFiles)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.InductionFiles.Count; i++)
            {
                string fileNameAux = command.InductionFiles[i].FileName
                    .Replace("\\", "-")
                    .Replace("/", "-")
                    .Replace(":", "-")
                    .Replace("*", "-")
                    .Replace("?", "-")
                    .Replace("\"", "-")
                    .Replace("<", "-")
                    .Replace(">", "-")
                    .Replace("|", "-")
                    .Replace("#", "-")
                    .Replace("$", "-");

                fileURL = await _amazonS3Service.UploadFile(command.InductionFiles[i], "InductionsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                UpdateInductionsObjectCommand temp = new
                (
                    command.InductionFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        UpdateInductionsCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            command.InductionId,
            command.Name,
            command.Description,
            command.HasChangedFiles,
            command.FileNamesDeleted,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPost("FinishByInductionId")]
    public async Task<IActionResult> FinishInduction(BaseFinishInductionCommand command)
    {

        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        FinishInductionCommand request = new
        (
            command.InductionId,
            collaboratorEmail
        );

        ErrorOr<bool> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpDelete("DeleteInduction")]
    public async Task<IActionResult> DeleteInduction(DeleteBaseInductionCommand command)
    {
        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteInductionCommand request = new
        (
            companyResult.Value.Id,
            command.InductionId,
            collaboratorEmail
        );

        ErrorOr<bool> result = await _mediator.Send(request);
        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetInductionByCollaboratorId")]
    public async Task<IActionResult> GetInductionByCollaboratorId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<InductionForCollaboratorResponse>> result = await _mediator.Send(new GetInductionByCollaboratorIdQuery(companyResult.Value.Id, companyEmail));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetAllCollaboratorsInInductions")]
    public async Task<IActionResult> GetAllCollaboratorsInInductions([FromBody] GetAllCollaboratorInductionsQuery command)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<CollaboratorListResponse>> result = await _mediator.Send(command);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }
}