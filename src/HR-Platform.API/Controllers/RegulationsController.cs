using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.Regulations.Delete;
using HR_Platform.Application.Regulations.Common;
using HR_Platform.Application.Regulations.Create;
using HR_Platform.Application.Regulations.GetByCompanyId;
using HR_Platform.Application.Regulations.GetById;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HR_Platform.Application.Regulations.GetYearsByCompanyId;

namespace HR_Platform.API.Controllers;

[Route("Regulation")]
public class Regulation(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetRegulationsByCompanyId")]
    public async Task<IActionResult> GetRegulationsByCompanyId([FromBody] GetBaseRegulationByCompanyIdQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
        
        ErrorOr<RegulationFileAndYearListResponse> result = await _mediator.Send(new GetRegulationByCompanyIdQuery(companyResult.Value.Id, query.Year));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetRegulationYearsByCompanyId")]
    public async Task<IActionResult> GetRegulationYearsByCompanyId()
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

        ErrorOr<RegulationFileYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpPost("CreateRegulations")]
    public async Task<IActionResult> Create([FromForm] CreateBaseRegulationCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateRegulationsObjectCommand> formatFiles = [];

        if (command.RegulationFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.RegulationFiles.Count; i++)
            {
                string fileNameAux = command.RegulationFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.RegulationFiles[i], "RegulationsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateRegulationsObjectCommand temp = new
                (
                    command.RegulationFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateRegulationsCommand result = new
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
    public async Task<IActionResult> Delete([FromBody] DeleteRegulationsCommand command)
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
            Regulations => Ok(Regulations),
            errors => Problem(errors)
        );
    }


    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetRegulationByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<RegulationFileResponse> regulationResult = await _mediator.Send(command);
        regulationResult.Match(
            regulation => Ok(regulation),
            errors => Problem(errors)
        );

        string fileURl = regulationResult.Value != null && !string.IsNullOrEmpty(regulationResult.Value.FileURL) ? regulationResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(regulationResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}