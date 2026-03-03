using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Create;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Delete;
using HR_Platform.Application.CoexistenceCommitteeMinutes.GetByCompanyId;
using HR_Platform.Application.CoexistenceCommitteeMinutes.GetById;
using HR_Platform.Application.CoexistenceCommitteeMinutes.GetYearsByCompanyId;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("CoexistenceCommitteeMinute")]
public class CoexistenceCommitteeMinute(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetCoexistenceCommitteeMinutesByCompanyId")]
    public async Task<IActionResult> GetCoexistenceCommitteeMinutesByCompanyId([FromBody] GetBaseCoexistenceCommitteeMinuteByCompanyIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetCoexistenceCommitteeMinuteByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id,
            baseQuery.Year
        );

        ErrorOr<CoexistenceCommitteeMinuteFileAndYearListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetCoexistenceCommitteeMinuteYearsByCompanyId")]
    public async Task<IActionResult> GetCoexistenceCommitteeMinuteYearsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetYearsByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id
        );

        ErrorOr<CoexistenceCommitteeMinuteYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpPost("CreateCoexistenceCommitteeMinuteFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseCoexistenceCommitteeMinuteCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateCoexistenceCommitteeMinutesObjectCommand> formatFiles = [];

        if (command.CoexistenceCommitteeMinuteFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.CoexistenceCommitteeMinuteFiles.Count; i++)
            {
                string fileNameAux = command.CoexistenceCommitteeMinuteFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.CoexistenceCommitteeMinuteFiles[i], "CoexistenceCommitteeMinutesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateCoexistenceCommitteeMinutesObjectCommand temp = new
                (
                    command.CoexistenceCommitteeMinuteFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateCoexistenceCommitteeMinutesCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCoexistenceCommitteeMinutesCommand command)
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
            CoexistenceCommitteeMinutes => Ok(CoexistenceCommitteeMinutes),
            errors => Problem(errors)
        );
    }

    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetCoexistenceCommitteeMinuteByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<CoexistenceCommitteeMinuteFileResponse> coexistenceCommitteeMinuteResult = await _mediator.Send(command);
        coexistenceCommitteeMinuteResult.Match(
            coexistenceCommitteeMinute => Ok(coexistenceCommitteeMinute),
            errors => Problem(errors)
        );

        string fileURl = coexistenceCommitteeMinuteResult.Value != null && !string.IsNullOrEmpty(coexistenceCommitteeMinuteResult.Value.FileURL) ? coexistenceCommitteeMinuteResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(coexistenceCommitteeMinuteResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}