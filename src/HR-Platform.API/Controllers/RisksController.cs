using ErrorOr;
using HR_Platform.API.Common.JWT;
using HR_Platform.API.Controllers;
using HR_Platform.Application.Companies.Common;
using HR_Platform.Application.Companies.GetByEmail;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Application.Risks.Update;
using HR_Platform.Application.Risks.Common;
using HR_Platform.Application.Risks.Create;
using HR_Platform.Application.Risks.GetAllByRiskType;
using HR_Platform.Application.Risks.GetById;
using HR_Platform.Application.Risks.UpdateIsVisible;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_Platform.Application.Risks.Delete;

namespace HR_Platform.API.Controllers;

[Route("Risk")]
public class Risk(
    ISender mediator,
    IAmazonS3Service amazonS3Service

    ) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IAmazonS3Service _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));

    [HttpPost("GetRisksById")]
    public async Task<IActionResult> GetRisksByCompanyId([FromBody] GetRiskByIdQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<RiskResponse> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpPost("GetAllRisksByRiskTypeId")]
    public async Task<IActionResult> GetAllRisksByRiskTypeId([FromBody] GetAllByRiskTypeQuery baseQuery)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<List<RiskResponse>> result = await _mediator.Send(baseQuery);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }


    [HttpPost("CreateRiskFile")]
    public async Task<IActionResult> Create([FromForm] CreateBaseRiskCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string imageURL = string.Empty;
        string imageName = string.Empty;
        string videoURL = string.Empty;
        string videoName = string.Empty;

        if (command.ImageFile != null)
        {
            string fileNameAux = command.ImageFile.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.ImageFile, "RiskFilesFolder");
            imageName = fileNameAux;
        }

        if (command.VideoFile != null)
        {
            string fileNameAux = command.VideoFile.FileName
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

            videoURL = await _amazonS3Service.UploadFile(command.VideoFile, "RiskFilesFolder");
            videoName = fileNameAux;
        }

        CreateRisksCommand result = new(
            companyResult.Value.Id,
            command.RiskTypeId,
            command.Name,
            command.Description,
            imageName,
            imageURL,
            videoURL,
            videoName
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateRisk")]
    public async Task<IActionResult> Update([FromForm] UpdateBaseRiskCommand command)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        string imageURL = string.Empty;
        string imageName = string.Empty;
        string videoURL = string.Empty;
        string videoName = string.Empty;

        if (command.ImageFile != null)
        {
            string fileNameAux = command.ImageFile.FileName
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

            imageURL = await _amazonS3Service.UploadFile(command.ImageFile, "RiskFilesFolder");
            imageName = fileNameAux;
        }

        if (command.VideoFile != null)
        {
            string fileNameAux = command.VideoFile.FileName
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

            videoURL = await _amazonS3Service.UploadFile(command.VideoFile, "RiskFilesFolder");
            videoName = fileNameAux;
        }

        UpdateRisksCommand result = new(
            companyResult.Value.Id,
            command.RiskId,
            command.Name,
            command.Description,
            command.IsUpdateImageFile,
            command.IsUpdateVideoFile,
            imageName,
            imageURL,
            videoURL,
            videoName
        );

        ErrorOr<bool> updateResponse = await _mediator.Send(result);

        return updateResponse.Match(
            companyId => Ok(companyId),
            errors => Problem(errors)
        );
    }

    [HttpPatch("UpdateIsVisibleById")]
    public async Task<IActionResult> UpdateIsVisibleById([FromBody] UpdateIsVisibleCommand baseCommand)
    {

        Token token = new();
        string companyEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(companyEmail);
        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        ErrorOr<bool> result = await _mediator.Send(baseCommand);

        return result.Match(
            assignations => Ok(assignations),
            errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BaseDeleteRisksCommand baseCommand)
    {
        Token token = new();

        string collaboratorEmail = token.GetEmailFromToken(Request);

        GetCompanyByEmailQuery emailQuery = new(collaboratorEmail);

        ErrorOr<CompaniesResponse> companyResult = await _mediator.Send(emailQuery);

        companyResult.Match(
            company => Ok(company),
            errors => Problem(errors)
        );

        DeleteRisksCommand command = new
        (
            baseCommand.RiskId,
            companyResult.Value.Id
        );

        ErrorOr<bool> updateResult = await _mediator.Send(command);

        return updateResult.Match(
            Risks => Ok(Risks),
            errors => Problem(errors)
        );
    }
}