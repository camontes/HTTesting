using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.WorkplaceInformations.Delete;
using HR_Platform.Application.WorkplaceInformations.Common;
using HR_Platform.Application.WorkplaceInformations.Create;
using HR_Platform.Application.WorkplaceInformations.GetByCollaboratorId;
using HR_Platform.Application.WorkplaceInformations.GetById;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("WorkplaceInformation")]
public class WorkplaceInformation(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetWorkplaceInformationFileByCollaboratorId")]
    public async Task<IActionResult> GetWorkplaceInformationsByCompanyId([FromBody] GetBaseWorkplaceInformationsByCollaboratorIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetWorkplaceInformationsByCollaboratorIdQuery request = new
        (
            baseCollaboratorsQuery.CollaboratorId,
            collaboratorEmail,
            baseCollaboratorsQuery.Year
        );

        ErrorOr<WorkplaceInformationsResponse> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateWorkplaceInformationFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseWorkplaceInformationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<FileWorkplaceInformationFormatResponse> formatFiles = [];

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

                fileURL = await _amazonS3Service.UploadFile(command.Files[i], "WorkplaceInformationsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                FileWorkplaceInformationFormatResponse temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateWorkplaceInformationsCommand result = new(
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
    public async Task<IActionResult> Delete([FromBody] DeleteWorkplaceInformationsCommand command)
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
            WorkplaceInformations => Ok(WorkplaceInformations),
            errors => Problem(errors)
        );
    }


    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetWorkplaceInformationByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<WorkplaceInformationFileResponse> minuteResult = await _mediator.Send(command);
        minuteResult.Match(
            minute => Ok(minute),
            errors => Problem(errors)
        );

        string fileURl = minuteResult.Value != null && !string.IsNullOrEmpty(minuteResult.Value.FileURL) ? minuteResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(minuteResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}