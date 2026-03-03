using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.GetByEmail;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Application.WorkplaceRecommendations.Create;
using HR_Platform.Application.WorkplaceRecommendations.GetByCollaboratorId;
using HR_Platform.Application.WorkplaceRecommendations.GetById;
using HR_Platform.Application.WorkplaceRecommendations.GetYearsByCollaboratorId;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("WorkplaceRecommendation")]
public class WorkplaceRecommendation(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetWorkplaceRecommendationFileByCollaboratorId")]
    public async Task<IActionResult> GetWorkplaceRecommendationsByCompanyId([FromBody] GetBaseWorkplaceRecommendationsByCollaboratorIdQuery baseQuery)
    {

        Token token = new();
        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetWorkplaceRecommendationsByCollaboratorIdQuery request = new
        (
            baseQuery.CollaboratorId,
            baseQuery.Year,
            collaboratorEmail
        );

        ErrorOr<WorkplaceRecommendationsResponse> result = await _mediator.Send(request);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetWorkplaceRecommendationFileYearsByCollaboratorId")]
    public async Task<IActionResult> GetWorkplaceRecommendationFileYearsByCollaboratorId()
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

        GetWorkplaceRecommendationYearsByCollaboratorIdQuery resultQuery = new(collaboratorResult.Value.Id);

        ErrorOr<WorkplaceRecommendationFileYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpPost("CreateWorkplaceRecommendationFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseWorkplaceRecommendationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<FileWorkplaceRecommendationFormatResponse> formatFiles = [];

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

                fileURL = await _amazonS3Service.UploadFile(command.Files[i], "WorkplaceRecommendationsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                FileWorkplaceRecommendationFormatResponse temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateWorkplaceRecommendationsCommand result = new(
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

    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetWorkplaceRecommendationByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<WorkplaceRecommendationFileResponse> workplaceRecommendationResult = await _mediator.Send(command);
        workplaceRecommendationResult.Match(
            workplaceRecommendation => Ok(workplaceRecommendation),
            errors => Problem(errors)
        );

        string fileURl = workplaceRecommendationResult.Value != null && !string.IsNullOrEmpty(workplaceRecommendationResult.Value.FileURL) ? workplaceRecommendationResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(workplaceRecommendationResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}