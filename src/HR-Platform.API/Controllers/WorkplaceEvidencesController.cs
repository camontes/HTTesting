using ErrorOr;
using HR_Platform.API.Common.JWT; 
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Application.WorkplaceEvidences.Create;
using HR_Platform.Application.WorkplaceEvidences.GetByCollaboratorId;
using HR_Platform.Application.WorkplaceEvidences.GetById;
using HR_Platform.Application.WorkplaceEvidences.Delete;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HR_Platform.Application.WorkplaceEvidences.GetYearsByCollaboratorId;
using HR_Platform.Application.BirthdayTemplateSettings.GetByCompanyId;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Collaborators.Common;

namespace HR_Platform.API.Controllers;

[Route("WorkplaceEvidence")]
public class WorkplaceEvidence(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetWorkplaceEvidenceFileByCollaboratorId")]
    public async Task<IActionResult> GetWorkplaceEvidencesByCompanyId([FromBody] GetBaseWorkplaceEvidencesByCollaboratorIdQuery baseQuery)
    {

        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetWorkplaceEvidencesByCollaboratorIdQuery request = new
        (
            baseQuery.CollaboratorId,
            baseQuery.Year,
            collaboratorEmail
        );

        ErrorOr<WorkplaceEvidencesResponse> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetWorkplaceEvidenceFileYearsByCollaboratorId")]
    public async Task<IActionResult> GetWorkplaceEvidenceFileYearsByCollaboratorId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCollaboratorByEmailQuery collaboratorQuery = new(companyEmail);
        ErrorOr<CollaboratorsResponse> collaboratorResult = await _mediator.Send(collaboratorQuery);

        collaboratorResult.Match(
            collaborator => Ok(collaborator),
            errors => Problem(errors)
        );

        GetWorkplaceEvidenceYearsByCollaboratorIdQuery resultQuery = new(collaboratorResult.Value.Id);

        ErrorOr<WorkplaceEvidenceFileYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpPost("CreateWorkplaceEvidenceFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseWorkplaceEvidenceCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<FileWorkplaceEvidenceFormatResponse> formatFiles = [];

        if (command.Files is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.Files.Count; i++)
            {
                string fileNameAux = command.Files[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.Files[i], "WorkplaceEvidencesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                FileWorkplaceEvidenceFormatResponse temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateWorkplaceEvidencesCommand result = new(
            collaboratorEmail,
            command.CollaboratorId,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteWorkplaceEvidencesCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            WorkplaceEvidences => Ok(WorkplaceEvidences),
            errors => Problem(errors)
        );
    }

    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetWorkplaceEvidenceByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<WorkplaceEvidenceFileResponse> workplaceEvidenceResult = await _mediator.Send(command);
        workplaceEvidenceResult.Match(
            workplaceEvidence => Ok(workplaceEvidence),
            errors => Problem(errors)
        );

        string fileURl = workplaceEvidenceResult.Value != null && !string.IsNullOrEmpty(workplaceEvidenceResult.Value.FileURL) ? workplaceEvidenceResult.Value.FileURL : string.Empty;
        byte[] byteArr;


        if (!string.IsNullOrEmpty(fileURl))
        {
            string[] fileURLSplit = fileURl.Split("/");

            string fileToDownload = string.Empty;

            if (fileURLSplit != null)
            {
                if (fileURLSplit.Length > 1)
                    fileToDownload = fileURLSplit[^2] + "/" + fileURLSplit[^1];
            }

            MemoryStream stream = await _amazonS3Service.GetFile(fileToDownload);

            using (MemoryStream memoryStream = new())
            {
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                byteArr = memoryStream.ToArray();
            }

            FileContentResult result = File(byteArr, "application/octet-stream");

            result.FileDownloadName = Regex.Replace(workplaceEvidenceResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}