using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.OccupationalTests.Delete;
using HR_Platform.Application.OccupationalTests.Common;
using HR_Platform.Application.OccupationalTests.Create;
using HR_Platform.Application.OccupationalTests.GetByCompanyId;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("OccupationalTest")]
public class OccupationalTest(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetOccupationalTestFileByCollaboratorId")]
    public async Task<IActionResult> GetOccupationalTestsByCompanyId([FromBody] GetOccupationalTestsByCollaboratorIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<OccupationalTestsResponse> result = await _mediator.Send(baseCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPost("CreateOccupationalTestFile")]
    public async Task<IActionResult> Create ([FromForm] CreateBaseOccupationalTestCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<FileFormatResponse> formatFiles = [];

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

                fileURL = await _amazonS3Service.UploadFile(command.Files[i], "OccupationalTestsFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                FileFormatResponse temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created",
                    command.FileTypeIds[i],
                    command.Others is not null && command.Others[i] is not null && !string.IsNullOrEmpty(command.Others[i]) ? command.Others[i] : string.Empty
                );
                formatFiles.Add(temp);
            }
        }

        CreateOccupationalTestsCommand result = new(
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

    [HttpPost("GetCollaboratorsByOccupationalTest")]
    public async Task<IActionResult> GetCollaboratorsByOccupationalTest(GetOccupationalTestsAllCollaboratorQuery query)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<AllCollaboratorsResponse> result = await _mediator.Send(query);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteOccupationalTestsCommand command)
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
            OccupationalTests => Ok(OccupationalTests),
            errors => Problem(errors)
        );
    }
}