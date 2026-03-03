using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.Application.BrigadeInventories.Common;
using HR_Platform.Application.BrigadeInventories.Create;
using HR_Platform.Application.BrigadeInventories.GetByCompanyId;
using HR_Platform.Application.BrigadeInventories.GetYearsByCompanyId;
using HR_Platform.Application.BrigadeInventories.MarkAsDeleted;
using HR_Platform.Application.BrigadeInventories.Update;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("BrigadeInventory")]
public class BrigadeInventory(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetBrigadeInventoryByCompanyId")]
    public async Task<IActionResult> GetBrigadeInventoryByCompanyId([FromBody] GetBaseBrigadeInventoryByCompanyIdQuery query)
    {
        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        GetBrigadeInventoryByCompanyIdQuery resultQuery = new(
            companyResult.Value.Id,
            query.Year
        );

        ErrorOr<FullBrigadeInventoryResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpGet("GetBrigadeInventoryYearsByCompanyId/{language}")]
    public async Task<IActionResult> GetBrigadeInventoryYearsByCompanyId(int language)
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
            language, // 1 = Spanish, 2 = English
            companyResult.Value.Id
        );

        ErrorOr<BrigadeInventoryYearsListResponse> result = await _mediator.Send(resultQuery);

        return result.Match(
            minuteYears => Ok(minuteYears),
            errors => Problem(errors));
    }

    [HttpPost("CreateBrigadeInventory")]
    public async Task<IActionResult> Create([FromForm] CreateBaseBrigadeInventoryCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CreateBrigadeInventoriesObjectCommand> formatFiles = [];

        if (command.BrigadeInventoryFiles is not null)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.BrigadeInventoryFiles.Count; i++)
            {
                string fileNameAux = command.BrigadeInventoryFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.BrigadeInventoryFiles[i], "BrigadeInventoryFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CreateBrigadeInventoriesObjectCommand temp = new
                (
                    command.BrigadeInventoryFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateBrigadeInventoriesCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            command.Name,
            command.Description,
            command.CompanyLocation,
            command.Amount,
            command.UnitMeasureId,
            command.Other,
            command.ApplyPurchaseDate,
            command.ApplyExpirationDate,
            command.PurchaseDate,
            command.ExpirationDate,
            command.Observations,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }


    [HttpPatch("UpdateBrigadeInventory")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseBrigadeInventoryCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<UpdateBrigadeInventoriesObjectCommand> formatFiles = [];

        if (command.BrigadeInventoryFiles is not null && command.HasChangedFiles)
        {
            string fileURL = string.Empty;
            string fileName = string.Empty;

            for (int i = 0; i < command.BrigadeInventoryFiles.Count; i++)
            {
                string fileNameAux = command.BrigadeInventoryFiles[i].FileName
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

                fileURL = await _amazonS3Service.UploadFile(command.BrigadeInventoryFiles[i], "BrigadeInventoryFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                UpdateBrigadeInventoriesObjectCommand temp = new
                (
                    command.BrigadeInventoryFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        UpdateBrigadeInventoriesCommand result = new
        (
            collaboratorEmail,
            command.Id,
            command.Name,
            command.Description,
            command.CompanyLocation,
            command.Amount,
            command.UnitMeasureId,
            command.ApplyPurchaseDate,
            command.ApplyExpirationDate,
            command.PurchaseDate,
            command.ExpirationDate,
            command.Observations,
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

    [HttpDelete("MarkAsDeleted")]
    public async Task<IActionResult> MarkAsDeleted([FromBody] MarkBrigateInventoryAsDeletedCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> deleteResponse = await _mediator.Send(command);

        if (deleteResponse.Value)
            return StatusCode(200, new { message = "Brigate Inventory deleted successfully", deleteResponse.Value });

        return StatusCode(400, new { message = "Brigate Inventory not deleted", deleteResponse.Value });
    }
}