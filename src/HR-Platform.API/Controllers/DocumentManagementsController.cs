using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.DocumentManagements.Common;
using HR_Platform.Application.DocumentManagements.Create;
using HR_Platform.Application.DocumentManagements.Delete;
using HR_Platform.Application.DocumentManagements.GetByCompanyId;
using HR_Platform.Application.DocumentManagements.GetById;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("DocumentManagement")]
public class DocumentManagement(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetDocumentManagementFileByCollaboratorId")]
    public async Task<IActionResult> GetDocumentManagementsByCompanyId([FromBody] GetDocumentManagementsByCollaboratorIdQuery baseCollaboratorsQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<DocumentManagementsResponse> result = await _mediator.Send(baseCollaboratorsQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("CreateDocumentManagement")]
    public async Task<IActionResult> Create([FromForm] CreateBaseDocumentManagementCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<DocumentManagementFileFormatResponse> formatFiles = [];

        if (command.Files is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.Files.Count; i++)
            {
                fileURL = await _amazonS3Service.UploadFile(command.Files[i], "DocumentManagementsFolder");
                fileName = command.Files[i].FileName;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                DocumentManagementFileFormatResponse temp = new
                (
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created",
                    command.FileTypeIds[i],
                    command.Others is not null && command.Others[i] is not null && !string.IsNullOrEmpty(command.Others[i]) ? command.Others[i] : string.Empty
                );
                formatFiles.Add(temp);
            }
        }

        CreateDocumentManagementsCommand result = new(
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
    public async Task<IActionResult> Delete([FromBody] DeleteDocumentManagementsCommand command)
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
            DocumentManagements => Ok(DocumentManagements),
            errors => Problem(errors)
        );
    }

    [HttpPost("DownloadFile")]
    public async Task<FileResult> Download([FromBody] GetDocumentManagementByIdQuery command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<DocumentManagementFileResponse> documentManagementResult = await _mediator.Send(command);
        documentManagementResult.Match(
            documentManagement => Ok(documentManagement),
            errors => Problem(errors)
        );

        string fileURl = documentManagementResult.Value != null && !string.IsNullOrEmpty(documentManagementResult.Value.FileURL) ? documentManagementResult.Value.FileURL : string.Empty;
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

            result.FileDownloadName = Regex.Replace(documentManagementResult.Value.FileName, @"[^0-9a-zA-Z.]+", "");

            return result;
        }
        return null;
    }
}