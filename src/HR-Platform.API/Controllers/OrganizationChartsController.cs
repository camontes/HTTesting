using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.OrganizationCharts.Delete;
using HR_Platform.Application.OrganizationCharts.Common;
using HR_Platform.Application.OrganizationCharts.Create;
using HR_Platform.Application.OrganizationCharts.GetByCompanyId;
using HR_Platform.Application.OrganizationCharts.GetById;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("OrganizationChart")]
public class OrganizationChart(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpGet("GetOrganizationChartsByCompanyId")]
    public async Task<IActionResult> GetOrganizationChartsByCompanyId()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<OrganizationChartFileResponse> result = await _mediator.Send(new GetOrganizationChartByCompanyIdQuery(companyResult.Value.Id));

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPost("CreateOrganizationChartFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseOrganizationChartCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string fileURL = !string.IsNullOrEmpty(command.FileURL) ? command.FileURL : string.Empty;
        string fileName =  string.Empty;

        if (command.OrganizationChartFiles is not null)
        {
            string fileNameAux = command.OrganizationChartFiles.FileName
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

            fileURL = await _amazonS3Service.UploadFile(command.OrganizationChartFiles, "OrganizationChartsFolder");
            fileName = fileNameAux;

            if (string.IsNullOrEmpty(fileURL))
                return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

        }

        CreateOrganizationChartsCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            command.IsByFile,
            command.IsByUrl,
            fileName,
            fileURL
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteOrganizationChartsCommand command)
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
            OrganizationCharts => Ok(OrganizationCharts),
            errors => Problem(errors)
        );
    }

    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetOrganizationChartByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<OrganizationChartFileResponse> organizationChartResult = await _mediator.Send(command);
        organizationChartResult.Match(
            organizationChart => Ok(organizationChart),
            errors => Problem(errors)
        );

        string fileURl = organizationChartResult.Value != null && !string.IsNullOrEmpty(organizationChartResult.Value.FileURL) ? organizationChartResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(organizationChartResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }

}