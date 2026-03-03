using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.BenefitClaims.Common;
using HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;
using HR_Platform.Application.CollaboratorBrigadeInventories.Common;
using HR_Platform.Application.CollaboratorBrigadeInventories.Create;
using HR_Platform.Application.CollaboratorBrigadeInventories.GetByCompanyId;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Platform.API.Controllers;

[Route("CollaboratorBrigadeInventory")]
public class CollaboratorBrigadeInventory(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("CreateCollaboratorBrigadeInventory")]
    public async Task<IActionResult> Create([FromForm] CreateBaseCollaboratorBrigadeInventoryCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        List<CollaboratorBrigadeInventoryObject> formatFiles = [];

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

                fileURL = await _amazonS3Service.UploadFile(command.BrigadeInventoryFiles[i], "CollaboratorBrigadeInventoriesFolder");
                fileName = fileNameAux;

                if (string.IsNullOrEmpty(fileURL))
                    return StatusCode(400, new { message = $"It´s not possible create a URL for {fileName}" });

                CollaboratorBrigadeInventoryObject temp = new
                (
                    command.BrigadeInventoryFiles[i],
                    fileName is not null && !string.IsNullOrEmpty(fileName) ? fileName : string.Empty,
                    fileURL is not null && !string.IsNullOrEmpty(fileURL) ? fileURL : "Not created"
                );
                formatFiles.Add(temp);
            }
        }

        CreateCollaboratorBrigadeInventoryCommand result = new
        (
            collaboratorEmail,
            companyResult.Value.Id,
            command.BrigadeMemberIdList,
            command.SendAllBrigades,
            command.GeneralBrigadeInventoryId,
            command.Amount,
            command.UnitMeasureId,
            command.DeliveryDate,
            command.ApplyReturnDate,
            command.ReturnDate,
            command.Observations,
            formatFiles
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }


    [HttpGet("GetBrigadeMemberWithInventary")]
    public async Task<IActionResult> GetBrigadeMemberWithInventary()
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);
        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<BrigadeStaffingResponse>> createResult = await _mediator.Send(new GetCollaboratorBrigadeInventoryByCompanyIdQuery());

        return createResult.Match(
           companyId => Ok(companyId),
           errors => Problem(errors)
       );
    }
}